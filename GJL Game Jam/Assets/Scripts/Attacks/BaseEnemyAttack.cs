using GameEngine.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyAttack : BaseCommand
{

    private float jumpHeight=20f;
    private float checkRadius = 2f;
    private float lastAttackTime;
    private float attackDelay = 2f;


    float jumpCountdown;
    public override void StartCommand()
    {
        lastAttackTime = attackDelay;
        
    }
    public override void Execute()
    {
        if (!IsGrounded())
        {
            owner.Anim.ResetTrigger("Idle");
        }
        else
        {
            owner.Anim.SetTrigger("Idle");
        }
        if (lastAttackTime > 0)
        {
            lastAttackTime -= Time.deltaTime;

        }
        else
        {
            float distanceX = owner.Target.position.x - owner.Transform.position.x;

            if (IsGrounded())
            {
                Jump(distanceX);
            }
               
            lastAttackTime = attackDelay;
        }
    }
    void Jump(float distanceX)
    {
        owner.RB.AddForce(new Vector2(distanceX, jumpHeight), ForceMode2D.Impulse);
        owner.Anim.SetTrigger("Jump");
    }
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(owner.Transform.position, checkRadius, Globals.Instance.whatIsGround);
    }

    public override void ExitCommand()
    {
        owner.Anim.ResetTrigger("Jump");

    }
}
