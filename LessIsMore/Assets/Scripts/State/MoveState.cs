using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    private CharacterController2D owner;
    private float startDashTime;

    public MoveState(CharacterController2D owner)
    {
        this.owner = owner;
    }
    public void Enter()
    {
        //start animation
        Animate();
        startDashTime = Time.time;
    }

    public void Execute()
    {

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(owner.movement.x * 10f, owner.movement.y * 10f);
        // And then smoothing it out and applying it to the character
        owner.RB.velocity = Vector3.SmoothDamp(owner.RB.velocity, targetVelocity, ref owner.m_Velocity, owner.m_MovementSmoothing);
        
        if (Input.GetKey(KeyCode.LeftShift))
            owner.RequestChangeState(new SprintState(owner));
    }
   /// <summary>
   /// ANIMATE THE PLAYER WHEN MOVING
   /// </summary>
    private void Animate()
    {
        owner.animator.SetFloat("Horizontal", owner.direction.x);
        owner.animator.SetFloat("Vertical", owner.direction.y);
        owner.animator.SetFloat("Speed", owner.movement.sqrMagnitude);
    }
    public void Exit()
    {
        
    }
}
