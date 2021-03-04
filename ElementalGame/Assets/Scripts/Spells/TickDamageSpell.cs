using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickDamageSpell : PrefabSpell
{
    private ElementDatabase edb;
    void Start()
    {
        edb = ElementDatabase.instance;
    }
    float lastTick;
    void OnTriggerStay(Collider collider)
    {
        if (!collider.isTrigger)
        {
            if (collider.CompareTag("Enemy"))
            {
                CurrentHealth recieveDamage = collider.gameObject.GetComponent<CurrentHealth>();
                ElementType targetElement = collider.gameObject.GetComponent<Stats>().element;
                float damageModifier = edb.CalculateDamage(spellElement, targetElement);
                float spellDamage = damageModifier* damage;
                if (Time.time > lastTick + tickDelay)
                {
                    lastTick = Time.time;
                    recieveDamage.TakeDamage(spellDamage);
                }
            }
        }
    }

}
