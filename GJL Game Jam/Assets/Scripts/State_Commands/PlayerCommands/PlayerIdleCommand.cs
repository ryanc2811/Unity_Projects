using GameEngine.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleCommand : BaseCommand
{
    public override void StartCommand()
    {
        owner.SetVelocity(0,0);
        owner.Anim.SetBool("Moving", false);
    }
    public override void Execute()
    {
        
    }
}
