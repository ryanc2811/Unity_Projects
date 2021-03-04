using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public float attackSpeed;
    public float maxHealth;
    public float baseMaxHealth;
    public float attackDamage;
    public float critChance;
    public float dropChance;
    public float moveSpeed;
    public float baseMoveSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    void Start()
    {
        EventManager.instance.OnAmmoChangedTrigger += OnAmmoChangedTrigger;
        maxHealth = baseMaxHealth;
        moveSpeed = baseMoveSpeed;
    }

    private void OnAmmoChangedTrigger(float ammoPercent)
    {
        Debug.Log(ammoPercent);
        maxHealth= (baseMaxHealth/ammoPercent);
        moveSpeed =(baseMoveSpeed /ammoPercent);
        
        EventManager.instance.PlayerStatsChanged();
    }
}
