using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    private List<GameObject> zombies = new List<GameObject>();
    public ResurrectUI resurrectUI;
    private IZombieFactory zombieFactory;
    public GameObject zombiePrefab;
    public static ZombieManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        EventManager.instance.OnAIDeathTrigger += OnAIDeath;
        EventManager.instance.OnPlayerDeathTrigger += OnPlayerDeath;
        zombieFactory = GetComponent<IZombieFactory>();
    }

    // Update is called once per frame
    void OnAIDeath(GameObject ai)
    {
        if(ai.GetComponent<Stats>().entityType == EntityType.Enemy)
        {
            GameObject zombie=zombieFactory.CreateZombie(zombiePrefab,ai.transform);
            zombies.Add(zombie);
        }
        if(ai.GetComponent<Stats>().entityType == EntityType.Zombie)
        {
            zombies.Remove(ai);
        }
    }
    void OnPlayerDeath(GameObject player)
    {
        GameState.instance.PauseGame();
        if (zombies.Count > 0)
        {
            resurrectUI.PopulateUI(zombies);
        }
    }
}
