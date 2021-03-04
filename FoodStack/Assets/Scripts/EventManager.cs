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


    public event Action OnLevelEndedTrigger;
    public event Action OnLevelChangedTrigger;
    public event Action<IngredientType> OnIngredientHitPlayerTrigger;

    
    public void LevelChangedTrigger()
    {
        if (OnLevelChangedTrigger != null)
        {
            OnLevelChangedTrigger();
        }
    }
    
    
    public void LevelEndedTrigger()
    {
        if (OnLevelEndedTrigger != null)
        {
            OnLevelEndedTrigger();
        }
    }
    public void IngredientHitPlayerTrigger(IngredientType player)
    {
        if (OnIngredientHitPlayerTrigger != null)
        {
            OnIngredientHitPlayerTrigger(player);
        }
    }

}
