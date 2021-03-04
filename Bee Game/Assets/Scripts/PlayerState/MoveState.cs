using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    private PlayerController owner;

    public MoveState(PlayerController owner)
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
        //Move the character by finding the target velocity
        if (owner.direction.z != 0)
        {
            var newpos = owner.RB.position + owner.TF.forward * owner.direction.z * owner.Speed;

            owner.RB.MovePosition(newpos);
        }
        if (owner.direction.x != 0)
        {
            var newpos = owner.RB.position + owner.TF.right * owner.direction.x * owner.Speed;

            owner.RB.MovePosition(newpos);
        }
        owner.RB.velocity = new Vector3(0, owner.direction.y * owner.Speed, 0)*10;
    }
    /// <summary>
    /// ANIMATE THE PLAYER WHEN MOVING
    /// </summary>
    private void Animate()
    {
        //owner.animator.SetFloat("Move", owner.direction.z);
        //owner.animator.SetFloat("Strafe", owner.direction.x);
        //owner.animator.SetBool("Idle", false);
    }
    public void Exit()
    {

    }
}
