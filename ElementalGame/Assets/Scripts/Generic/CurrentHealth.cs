using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentHealth : MonoBehaviour
{
    public float health { get; private set; }
    public bool IsDead { get; private set; }
    private Stats stats;
    public event EventHandler OnHealthChanged;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
        health = stats.GetValue(AttributeType.MaxHealth);
    }
    public void TakeDamage(float damage)
    {
        if (health-damage > 0)
        {
            health -= damage;
            Debug.Log("Ow");
            if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        }
        else
        {
            health = 0;
            if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        }
        if (health <= 0)
        {
            if (gameObject.CompareTag("Player"))
                EventManager.instance.PlayerDeathTrigger(gameObject);
            else
            {
                EventManager.instance.AIDeathTrigger(gameObject);
                Destroy(gameObject);
            }
            IsDead = true;
            
        }
    }
    public float GetHealthPercent()
    {
        return health/stats.GetValue(AttributeType.MaxHealth);
    }
    public void Heal(float healthAdded)
    {
        if (health >= 0&&health+healthAdded<=stats.GetValue(AttributeType.MaxHealth))
        {
            health += healthAdded;
            Debug.Log("Healed");
            if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        }
        if(health + healthAdded > stats.GetValue(AttributeType.MaxHealth))
        {
            health = stats.GetValue(AttributeType.MaxHealth);
        }
    }
}
