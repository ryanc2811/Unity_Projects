using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentHealth : MonoBehaviour
{
    public float maxHealth;

    private float health;

    public Action<MorphType, float> OnDamageTaken;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void RequestDamage(MorphType damageType,float damage)
    {
        OnDamageTaken?.Invoke(damageType,damage);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Heal(float healthToGain)
    {
        health += healthToGain;
        if (health >=maxHealth)
        {
            health = maxHealth;
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

}
