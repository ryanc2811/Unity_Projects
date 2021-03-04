using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemiesToBeSpawned = new List<GameObject>();
    List<GameObject> enemiesInLevel = new List<GameObject>();
    private GameObject[] enemyPositions;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnAIDeathTrigger += OnEnemyDeath;
        enemyPositions = GameObject.FindGameObjectsWithTag("EnemyPosition");
        SpawnStartingEnemies();
    }
    public void SpawnStartingEnemies()
    {
        for (int i = 0; i < enemiesToBeSpawned.Count; i++)
        {
            SpawnEnemy(enemiesToBeSpawned[i], enemyPositions[i].transform.position);
        }
    }
    public void SpawnEnemy(GameObject enemy,Vector3 position)
    {
        GameObject newEnemy=Instantiate(enemy,position,Quaternion.identity,null);
        enemiesInLevel.Add(newEnemy);
    }
    void OnEnemyDeath(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            if (enemiesInLevel.Contains(enemy))
            {
               enemiesInLevel.Remove(enemy);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
