using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : AI
{
    bool canAttack = false;
    float lastAttack;
    bool trigger = false;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == target)
        {
            canAttack = true;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == target)
        {
            canAttack = false;
        }
    }

    void Attack()
    {
        float attackDelay = attributes.attackDelay;
        int attackDamage = attributes.attackDamage;
        if (canAttack)
        {
            if (Time.time > lastAttack + attackDelay)
            {
                lastAttack = Time.time;
                target.GetComponent<ReceiveDamage>().TakeDamage(attackDamage);
            }
        }
    }
    void Patrol()
    {

    }
    void Update()
    {
        bool isDead = GetComponentInParent<ReceiveDamage>().IsDead;
        if (isDead && !trigger)
        {
            trigger = true;
        }

        target = GetComponentInParent<TargetEnemy>().currentTarget;
        
        if (target)
        {
            Attack();
        }
        else
        {
            Patrol();
        }
          
    }
}
