using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Attributes attributes;
    bool canAttack = false;
    float attackDelay;
    int attackDamage;
    GameObject attackTarget;
    float nextAttack;
    // Start is called before the first frame update
    void Start()
    {
        attributes = GetComponentInParent<Attributes>();
    }

    // Update is called once per frame
    void Update()
    {
        AttackEnemy();
    }
    void AttackEnemy()
    {
        attackDelay = attributes.attackDelay;
        attackDamage = attributes.attackDamage;

        if (canAttack)
        {
            if (nextAttack + attackDelay < Time.time)
            {
                nextAttack = Time.time;
                if (attackTarget)
                {
                    attackTarget.GetComponent<ReceiveDamage>().TakeDamage(attackDamage);
                }
            }
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&&!collision.isTrigger)
        {
            canAttack = true;
            attackTarget = collision.gameObject;
        }
           
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&& !collision.isTrigger)
        {
            canAttack = false;
            attackTarget = null;
        }
            
    }
}
