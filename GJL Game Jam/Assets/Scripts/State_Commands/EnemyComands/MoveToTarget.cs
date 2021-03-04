using GameEngine.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : BaseCommand
{
    float speed = 100f;

    public override void StartCommand()
    {
        owner.Anim.ResetTrigger("Idle");
    }

    public override void FixedExecute()
    {
        float velocityX= owner.Target.position.x - owner.Transform.position.x;
        owner.Anim.SetTrigger("Move");


        owner.SetVelocity(velocityX * speed * Time.fixedDeltaTime, owner.RB.velocity.y);

        if (velocityX > 0)
            owner.Transform.eulerAngles = new Vector3(0, 0, 0);
        else if (velocityX < 0)
            owner.Transform.eulerAngles = new Vector3(0, 180, 0);
    }

    public override void Execute()
    {
        
    }

    public override void ExitCommand()
    {
        owner.Anim.ResetTrigger("Move");
    }
}
