using GameEngine.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpCommand : BaseCommand
{
    private float jumpForce=10f;

    private float jumpTimeCounter;
    private float jumpTime = .4f;
    public override void StartCommand()
    {
        owner.RB.velocity = Vector2.up * jumpForce;
        jumpTimeCounter = jumpTime;
        
    }
    public override void Execute()
    {
        if (jumpTimeCounter > 0&& Input.GetKey(KeyCode.Space))
        {
            owner.RB.velocity = Vector2.up * jumpForce;
            jumpTimeCounter -= Time.deltaTime;
        }
    }
}
