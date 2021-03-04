using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> onPlayerMoveTrigger;
    public event Action<Zombie> onZombieSpawnTrigger;
    public event Action<Zombie> onZombieDeathTrigger;
    public void PlayerMoveTrigger(int id)
    {
        if (onPlayerMoveTrigger != null)
        {
            onPlayerMoveTrigger(id);
        }
    }
    public void ZombieSpawnTrigger(Zombie zombie)
    {
        if (onZombieSpawnTrigger != null)
        {
            onZombieSpawnTrigger(zombie);
        }
    }
    public void ZombieDeathTrigger(Zombie zombie)
    {
        if (onZombieDeathTrigger != null)
        {
            onZombieDeathTrigger(zombie);
        }
    }

}
