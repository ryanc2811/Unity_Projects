using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// SPRINT STATE FOR PLAYER
/// </summary>
public class SprintState : IState
{

    private float sprintSpeed = 15;
    private IController owner;
    public SprintState(IController owner)
    {
        this.owner = owner;
    }
    public void Enter()
    {
       
    }

    public void Execute()
    {
        //PLAYER IS SPRINTING
        Vector2 targetVelocity = new Vector2(owner.movement.x * sprintSpeed, owner.movement.y * sprintSpeed);
        owner.RB.velocity = targetVelocity;

    }
    
    public void Exit()
    {

    }


}
