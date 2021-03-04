using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private int damage;
    public void Initialize(int damage)
    {
        this.damage = damage;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("Turned"))
        {
            collider.gameObject.GetComponent<CurrentHealth>().TakeDamage(damage);
        }
        if (!collider.CompareTag("Enemy") && !collider.CompareTag(gameObject.tag) && !collider.isTrigger)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject,5f);
        }
        
    }
}
