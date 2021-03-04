using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (!collider.isTrigger)
        {
            if (!collider.gameObject.CompareTag("Enemy"))
            {
                EventManager.instance.BulletDestroyedTrigger();
            }
            else
            {
                EventManager.instance.EnemyKilledTrigger();
                Destroy(collider.gameObject, 0.2f);
            }
        }
    }
}
