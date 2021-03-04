using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private IController owner;
    public IdleState(IController owner)
    {
        this.owner = owner;
    }
    public void Enter()
    {
        //start animation
        Animate();
    }

    public void Execute()
    {
        //PLAYER IS IDLE
        if(owner is CharacterController2D)
        {
            ((CharacterController2D)owner).RB.velocity = Vector2.zero;
        }
    }

    public void Exit()
    {
        
    }
    private void Animate()
    {
        if (owner is CharacterController2D)
        {
            ((CharacterController2D)owner).animator.SetFloat("Horizontal", owner.direction.x);
            ((CharacterController2D)owner).animator.SetFloat("Vertical", owner.direction.y);
            ((CharacterController2D)owner).animator.SetFloat("Speed", owner.movement.sqrMagnitude);
        }
    }
}
