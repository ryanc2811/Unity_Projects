using GameEngine.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCommand : BaseCommand
{
    private float velocityX;
    private float speed=5f;
    public override void Execute()
    {
        velocityX = Input.GetAxis("Horizontal");
        owner.Anim.SetBool("Moving", true);
    }

    public override void FixedExecute()
    {
        owner.SetVelocity(velocityX*speed, owner.RB.velocity.y);
        if (velocityX > 0)
            owner.Transform.eulerAngles = new Vector3(0, 0, 0);
        else if (velocityX < 0)
            owner.Transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
