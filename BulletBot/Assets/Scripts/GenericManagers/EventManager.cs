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

    public event Action OnNewSceneLoadingTrigger;
    public event Action OnStageFailedTrigger;
    public event Action OnStagePassedTrigger;
    public event Action OnLevelChangedTrigger;
    public event Action OnBulletDestroyedTrigger;
    public event Action OnEnemyKilledTrigger;
    public event Action OnEnemySpawnedTrigger;
    public event Action<GameObject,Vector2> OnNewBulletTrigger;
    public event Action OnAmmoSpentTrigger;
    public event Action<int>OnAmmoCountSetTrigger;
    public event Action<int> OnXpAddedTrigger;
    public event Action<int> OnDoorOpenTrigger;
    public event Action<int> OnDoorCloseTrigger;

    public void DoorOpenTrigger(int id)
    {
        if (OnDoorOpenTrigger != null)
        {
            OnDoorOpenTrigger(id);
        }
    }
    public void DoorCloseTrigger(int id)
    {
        if (OnDoorCloseTrigger != null)
        {
            OnDoorCloseTrigger(id);
        }
    }
    public void XpAddedTrigger(int xp)
    {
        if (OnXpAddedTrigger != null)
        {
            OnXpAddedTrigger(xp);
        }
    }
    public void AmmoCountSetTrigger(int ammoCount)
    {
        if (OnAmmoCountSetTrigger != null)
        {
            OnAmmoCountSetTrigger(ammoCount);
        }
    }
    public void AmmoSpentTrigger()
    {
        if (OnAmmoSpentTrigger != null)
        {
            OnAmmoSpentTrigger();
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
    public void LevelChangedTrigger()
    {
        if (OnLevelChangedTrigger != null)
        {
            OnLevelChangedTrigger();
        }
    }
    public void StagePassedTrigger()
    {
        if (OnStagePassedTrigger != null)
        {
            OnStagePassedTrigger();
        }
    }
    public void StageFailedTrigger()
    {
        if (OnStageFailedTrigger != null)
        {
            OnStageFailedTrigger();
        }
    }
    public void NewBulletTrigger(GameObject newBullet,Vector2 direction)
    {
        if (OnNewBulletTrigger != null)
        {
            OnNewBulletTrigger(newBullet,direction);
        }
    }
    public void BulletDestroyedTrigger()
    {
        if (OnBulletDestroyedTrigger != null)
        {
            OnBulletDestroyedTrigger();
        }
    }

}
