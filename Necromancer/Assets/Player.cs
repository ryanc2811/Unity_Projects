using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CurrentHealth currentHealth;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = GetComponent<CurrentHealth>();
        EventManager.instance.OnHealPlayerTrigger += HealPlayer;
        healthBar.Setup(currentHealth);
    }
    void HealPlayer(int health)
    {
        currentHealth.Heal(health);
    }
}
