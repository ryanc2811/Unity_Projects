using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

public class RoamState : IState, IRoamState
{
    private PathFollower pathFollower;
    public void Begin()
    {
        
    }

    public void End()
    {
        pathFollower.Stop();
    }

    public void Execute()
    {
        if(pathFollower)
            pathFollower.Move();
    }

    public void SetPathFollower(PathFollower pathFollower)
    {
        this.pathFollower = pathFollower;
    }
}
