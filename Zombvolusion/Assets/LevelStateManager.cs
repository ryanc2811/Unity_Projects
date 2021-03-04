using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateManager : MonoBehaviour
{
    public int enemiesInLevel;
    bool trigger = false;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnAIDeathTrigger += OnEnemyDeath;
    }

    void OnEnemyDeath(GameObject ai)
    {
        if (ai.GetComponent<Attributes>().entityType == EntityType.Enemy)
        {
            enemiesInLevel--;
        }
    }

    void Update()
    {
        if (enemiesInLevel <= 0)
        {
            if (!trigger)
                EventManager.instance.LevelEndedTrigger();
        }
    }
}
