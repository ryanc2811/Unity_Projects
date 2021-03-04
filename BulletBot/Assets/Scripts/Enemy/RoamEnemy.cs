using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PathCreation.Examples;

public class RoamEnemy : Enemy
{
    private PathFollower pathFollower;
    protected override void Initialize()
    {
        pathFollower = GetComponent<PathFollower>();
        IRoamState roamState = new RoamState();
        roamState.SetPathFollower(pathFollower);
        controller.ChangeState((IState)roamState);

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IRoamState roamState = new RoamState();
            roamState.SetPathFollower(pathFollower);
            controller.ChangeState((IState)roamState);
        }
        if (Input.GetMouseButtonDown(1))
        {
            IState idleState = new IdleState();
            controller.ChangeState(idleState);
            
        }
    }
    
}
