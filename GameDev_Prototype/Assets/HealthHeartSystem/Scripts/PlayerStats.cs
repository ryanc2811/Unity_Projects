/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using System;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

    #region Sigleton
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerStats>();
            return instance;
        }
    }

    
    #endregion

    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxTotalHealth;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }

    [SerializeField]
    private int meat;
    public int Meat { get { return meat; } }
    public TextMeshProUGUI monsterMeatText;

    [SerializeField]
    private int ammo;
    public int Ammo { get { return ammo; } }
    public TextMeshProUGUI ammoText;

    public bool StopMoving = false;
    void Start()
    {
        UpdateMeat();
        UpdateAmmo();
    }
    public void Heal(float health)
    {
        this.health += health;
        ClampHealth();
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        ClampHealth();
    }

    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health = maxHealth;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }   
    }

    public void GiveMeat(int value)
    {
        if(meat<9)
            meat += value;
        UpdateMeat();
    }
    public void RemoveMeat(int value)
    {
        if(meat>0)
            meat -= value;
        UpdateMeat();
    }
    public void GiveAmmo(int value)
    {
        if (ammo < 9)
            ammo += value;
        UpdateAmmo();
    }
    public void RemoveAmmo(int value)
    {
        if (ammo > 0)
            ammo -= value;
        UpdateAmmo();
    }
    private void UpdateMeat()
    {
        monsterMeatText.text = "MEAT : " + meat;
    }
    private void UpdateAmmo()
    {
        ammoText.text = "AMMO : " + ammo;
    }
    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }
}
