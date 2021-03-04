using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEssence : MonoBehaviour
{
    private Transform player;
    int xpToBeAdded = 10;
    bool canMoveToPlayer = false;
    float speed=0.2f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        EventManager.instance.OnLevelEndedTrigger+=LevelEnded;
        rb = GetComponent<Rigidbody>();
        Dispurse();
    }
    void Dispurse()
    {
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        rb.AddForce(direction * 0.4f,ForceMode.Impulse);
    }
    void LevelEnded()
    {
        canMoveToPlayer = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (canMoveToPlayer)
        {
            transform.position=Vector3.MoveTowards(transform.position, player.position, speed);
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (canMoveToPlayer)
        {
            if (collider.CompareTag("Player"))
            {
                EventManager.instance.GivePlayerExperienceTrigger(xpToBeAdded);
                Destroy(gameObject);
            }
        }
    }
}
