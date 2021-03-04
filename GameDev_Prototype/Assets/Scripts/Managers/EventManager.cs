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

    public event Action OnScanRequiredTrigger;
    public event Action<int> OnDoorOpenTrigger;
    public event Action<int> OnDoorCloseTrigger;

    public void ScanRequiredTrigger()
    {
        if (OnScanRequiredTrigger != null)
        {
            OnScanRequiredTrigger();
        }
    }
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
}
