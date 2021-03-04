using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public event Action OnEnemyKilledTrigger;
    public event Action OnEnemySpawnedTrigger;
    public event Action<float> OnGunShotTrigger;
    public event Action<float> OnAmmoChangedTrigger;
    public event Action OnPlayerStatsChanged;

    public void PlayerStatsChanged()
    {
        if (OnPlayerStatsChanged != null)
        {
            OnPlayerStatsChanged();
        }
    }
    public void GunShotTrigger(float recoil)
    {
        if (OnGunShotTrigger != null)
        {
            OnGunShotTrigger(recoil);
        }
    }
    public void AmmoChangedTrigger(float ammoPercent)
    {
        if (OnAmmoChangedTrigger != null)
        {
            OnAmmoChangedTrigger(ammoPercent);
        }
    }
    public void EnemySpawnedTrigger()
    {
        if (OnEnemySpawnedTrigger != null)
        {
            OnEnemySpawnedTrigger();
        }
    }
    public void EnemyKilledTrigger()
    {
        if (OnEnemyKilledTrigger != null)
        {
            OnEnemyKilledTrigger();
        }
    }
   
}
