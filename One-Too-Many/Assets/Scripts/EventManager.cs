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

    public event Action <GameObject>OnPlayerDeathTrigger;
    public event Action OnLevelEndedTrigger;
    public event Action OnExperienceChangedTrigger;
    public event Action OnLevelChangedTrigger;
    public event Action<int> OnGivePlayerExperienceTrigger;

    public void GivePlayerExperienceTrigger(int xp)
    {
        if (OnGivePlayerExperienceTrigger != null)
        {
            OnGivePlayerExperienceTrigger(xp);
        }
    }
    public void LevelChangedTrigger()
    {
        if (OnLevelChangedTrigger != null)
        {
            OnLevelChangedTrigger();
        }
    }
    public void ExperienceChangedTrigger()
    {
        if (OnExperienceChangedTrigger != null)
        {
            OnExperienceChangedTrigger();
        }
    }
    
    public void LevelEndedTrigger()
    {
        if (OnLevelEndedTrigger != null)
        {
            OnLevelEndedTrigger();
        }
    }
    public void PlayerDeathTrigger(GameObject player)
    {
        if (OnPlayerDeathTrigger != null)
        {
            OnPlayerDeathTrigger(player);
        }
    }

}
