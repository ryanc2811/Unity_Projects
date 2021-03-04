using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMind : MonoBehaviour
{
    public List<Zombie> Zombies { get;private set; }
    
    // Start is called before the first frame update
    void Awake()
    {
        Zombies = new List<Zombie>();
    }
    void Start()
    {
        GameEvents.current.onZombieSpawnTrigger += OnZombieSpawn;
        GameEvents.current.onZombieDeathTrigger += OnZombieDeath;
    }
    private void OnZombieSpawn(Zombie zombie)
    {
        Debug.Log("Player " + zombie.name + " Spawned");
        Zombies.Add(zombie);
    }
    private void OnZombieDeath(Zombie zombie)
    {
        Debug.Log("Player " + zombie.name + " Died");
        Zombies.Remove(zombie);
    }
    private void OnDestroy()
    {
        GameEvents.current.onZombieSpawnTrigger -= OnZombieSpawn;
        GameEvents.current.onZombieDeathTrigger -= OnZombieDeath;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
