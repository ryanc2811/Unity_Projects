using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private CurrentHealth unitHealth;

    public int damage = 1;
    // Start is called before the first frame update
    void Awake()
    {
        unitHealth = GetComponent<CurrentHealth>();
    }

    public int GetCurrentHealth()
    {
        return unitHealth.currentHealth;
    }
    public bool CheckIsDead(int damage)
    {
        unitHealth.TakeDamage(damage);
        return unitHealth.currentHealth <= 0;
    }
    public int GetMaxHealth()
    {
        return unitHealth.maxHealth;
    }
}
