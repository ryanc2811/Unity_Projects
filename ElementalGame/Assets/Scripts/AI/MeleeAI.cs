using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : MonoBehaviour,IAIAttack
{
    public void Attack(GameObject target,int damage)
    {
        int attackDamage = damage;
        GameObject attackTarget = target;
        attackTarget.GetComponent<CurrentHealth>().TakeDamage(attackDamage);
    }

}
