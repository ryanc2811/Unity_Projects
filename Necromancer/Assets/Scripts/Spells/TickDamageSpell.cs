using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickDamageSpell : PrefabSpell
{
    float lastTick;
    void OnTriggerStay(Collider collider)
    {
        if (!collider.isTrigger)
        {
            if (collider.CompareTag("Enemy"))
            {
                CurrentHealth recieveDamage = collider.gameObject.GetComponent<CurrentHealth>();
                if (Time.time > lastTick + tickDelay)
                {
                    lastTick = Time.time;
                    recieveDamage.TakeDamage(damage);
                }
            }
        }
    }

}
