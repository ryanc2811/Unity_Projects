using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEssenceManager : MonoBehaviour
{
    private GameObject player;
    
    public GameObject soulPrefab;
    int maxSoulsDropped=10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EventManager.instance.OnAIDeathTrigger += OnEnemyDeath;
    }

    void OnEnemyDeath(GameObject ai)
    {
        if (ai.CompareTag("Enemy"))
        {
            int soulsDropped = Random.Range(1, maxSoulsDropped);
            for (int i = 0; i < soulsDropped; i++)
            {
                Instantiate(soulPrefab, ai.transform.position + new Vector3(Random.Range(0f, 3f), 2f,Random.Range(0f, 3f)), Quaternion.identity);
            }
        }
    }
}
