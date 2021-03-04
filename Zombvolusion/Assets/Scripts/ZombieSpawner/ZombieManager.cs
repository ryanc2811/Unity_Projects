using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    private List<GameObject> zombies = new List<GameObject>();
    public NewPlayerUI newPlayerUI;
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
        if(ai.GetComponent<Attributes>().entityType == EntityType.Enemy)
        {
            GameObject zombie=zombieFactory.CreateZombie(zombiePrefab,ai.transform);
            zombies.Add(zombie);
        }
        if(ai.GetComponent<Attributes>().entityType == EntityType.Zombie)
        {
            zombies.Remove(ai);
        }
    }
    void OnPlayerDeath(GameObject player)
    {
        Time.timeScale = 0;
        if (zombies.Count > 0)
        {
            if (zombies.Contains(player))
                zombies.Remove(player);
            newPlayerUI.PopulateUI(zombies);
            
            Debug.Log("Afafgga");
        }
            
    }
}
