using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherCurse : ICurseEffect
{
    private PlayerController controller;
    private GameObject player;
    int randomChance;
    bool canTether = false;
    float timeSinceTether;
    float timeTest;
    public void Initialize(GameObject player)
    {
        this.player = player;
        controller = PlayerController.instance;
    }

    public void ApplyCurse()
    {
        if (!canTether)
        {
            if (Time.time > timeSinceTether + 3f)
            {
                timeSinceTether = Time.time;
                TetherChance();
            }
        }
        else
        {
            TetherTimer();
        }
    }
    void TetherChance()
    {
        const float probabilityWindow = 10f;
        float randomChance = Random.Range(0, 100);

        if (randomChance < probabilityWindow)
        {
            timeSinceTether = Time.time;
            canTether = true;
        }
        
    }
    void TetherTimer()
    {
        controller.StopMoving(true);
        Debug.Log(Time.time);
        if (Time.time > timeSinceTether+3f)
        {
            timeSinceTether = Time.time;
            canTether = false;
            controller.StopMoving(false);
        }
    }
    public void RemoveCurse()
    {
        controller.StopMoving(false);
    }
}
