using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void LevelEnd()
    {
        EventManager.instance.LevelEndedTrigger();
    }
    public void SpawnEnemy()
    {
        EnemySpawner spawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        spawner.SpawnStartingEnemies();
    }
}
