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

    public event Action<GameObject> OnAIDeathTrigger;
    public event Action <GameObject>OnPlayerDeathTrigger;
    public event Action<GameObject> OnNewPlayerTrigger;
    public event Action OnLevelEndedTrigger;

    public void LevelEndedTrigger()
    {
        if (OnLevelEndedTrigger != null)
        {
            OnLevelEndedTrigger();
        }
    }
    public void AIDeathTrigger(GameObject GO)
    {
        if (OnAIDeathTrigger != null)
        {
            OnAIDeathTrigger(GO);
        }
    }
    public void PlayerDeathTrigger(GameObject player)
    {
        if (OnPlayerDeathTrigger != null)
        {
            OnPlayerDeathTrigger(player);
        }
    }
    public void NewPlayerTrigger(GameObject player)
    {
        if (OnNewPlayerTrigger != null)
        {
            OnNewPlayerTrigger(player);
        }
    }
}
