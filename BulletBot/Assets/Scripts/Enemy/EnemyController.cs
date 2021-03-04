using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private StateMachine stateMachine;
    private bool spawnTrigger = false;
    void Awake()
    {
        stateMachine = new StateMachine();
    }
    public void ChangeState(IState newState)
    {
        if(stateMachine!=null)
            stateMachine.ChangeState(newState);
    }
    // Update is called once per frame
    void Update()
    {
        if (!spawnTrigger)
        {
            EventManager.instance.EnemySpawnedTrigger();
            spawnTrigger = true;
        }
        if (stateMachine!=null)
            stateMachine.Update();
    }
}

