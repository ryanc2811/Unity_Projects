using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int enemiesLeft = 0;
    private LevelSystem levelSystem;
    private LevelSystemAnimated levelSystemAnimated;
    [SerializeField]
    private ExperienceUI xpUI;
    [SerializeField]
    private GameStatsUI gameStatsUI;
    private EventManager eventManager;
    void Awake()
    {
        //gameStatsUI = GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStatsUI>();
        levelSystem = new LevelSystem();
        levelSystemAnimated = new LevelSystemAnimated(levelSystem);
        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }
    void Start()
    {
        eventManager = EventManager.instance;
        xpUI.transform.parent.gameObject.SetActive(true);
        xpUI.SetLevelSystemAnimated(levelSystemAnimated);
        xpUI.transform.parent.gameObject.SetActive(false);
        eventManager.OnEnemyKilledTrigger += EnemyKilled;
        eventManager.OnEnemySpawnedTrigger += EnemySpawned;

        eventManager.OnAmmoCountSetTrigger += OnAmmoCountSetTrigger;
        eventManager.OnAmmoSpentTrigger += OnAmmoSpentTrigger;
        eventManager.OnBulletDestroyedTrigger += OnBulletDestroyedTrigger;
        eventManager.OnXpAddedTrigger += AddXp;
        
    }
    private void AddXp(int xp)
    {
        levelSystem.AddExperience(xp);
    }
    private void OnBulletDestroyedTrigger()
    {
        Time.timeScale = 1;
        if (gameStatsUI.ammoCount == 0)
            eventManager.StageFailedTrigger();
    }

    private void OnAmmoSpentTrigger()
    {
        gameStatsUI.AmmoSpent();
    }

    private void OnAmmoCountSetTrigger(int ammoCount)
    {
        gameStatsUI.NewAmmoCountSet(ammoCount);
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        PlayerPrefs.SetInt("Level", levelSystem.GetLevel());
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        PlayerPrefs.SetInt("XP", levelSystem.GetExperience());
    }
    void EnemySpawned()
    {
        enemiesLeft++;
    }
    void EnemyKilled()
    {
        enemiesLeft--;
        if (enemiesLeft == 0)
        {
            eventManager.StagePassedTrigger();
            gameStatsUI.gameObject.SetActive(false);
        }
        else
        {
            CameraShake.instance.Shake(0.5f,0.2f,1f);
            StartCoroutine(SlowTime());
        }
    }
    IEnumerator SlowTime()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1f;
    }
    void OnDestroy()
    {
        eventManager.OnEnemyKilledTrigger -= EnemyKilled;
        eventManager.OnEnemySpawnedTrigger -= EnemySpawned;

        eventManager.OnAmmoCountSetTrigger -= OnAmmoCountSetTrigger;
        eventManager.OnAmmoSpentTrigger -= OnAmmoSpentTrigger;
        eventManager.OnBulletDestroyedTrigger -= OnBulletDestroyedTrigger;
        eventManager.OnXpAddedTrigger -= AddXp;
    }
}
