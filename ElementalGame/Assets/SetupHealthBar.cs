using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupHealthBar : MonoBehaviour
{
    CurrentHealth currentHealth;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = GetComponent<CurrentHealth>();
        healthBar.Setup(currentHealth);
    }
}
