using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private PlayerController owner;
    public IdleState(PlayerController owner)
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
        owner.RB.velocity = Vector3.zero;
    }

    public void Exit()
    {

    }
    private void Animate()
    {
        //owner.animator.SetFloat("Horizontal", owner.direction.x);
        //owner.animator.SetFloat("Vertical", owner.direction.y);
        //owner.animator.SetFloat("Speed", owner.Speed);
        //owner.animator.SetBool("Idle", true);
    }
}
