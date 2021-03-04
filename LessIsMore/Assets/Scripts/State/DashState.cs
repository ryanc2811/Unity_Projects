using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DASHES THE PLAYER IN A DIRECTION
/// </summary>
public class DashState :IState 
{

    private IController owner;
    private float dashTime;
    private float startDashTime = 3f;
    private float dashSpeed = 100f;
    public DashState(IController owner)
    {
        this.owner = owner;
    }
    public void Enter()
    {
        dashTime = startDashTime;
        owner.TF.GetComponent<BoxCollider2D>().enabled=false;

    }

    public void Execute()
    {
        //DASH THE PLAYER
        if (dashTime <= 0)
        {
            owner.movement=Vector2.zero;
            owner.RB.velocity = Vector2.zero;
        }
        else
        {
            dashTime -= Time.deltaTime;
            owner.RB.velocity = owner.movement * dashSpeed;
        }
        
    }
   
    public void Exit()
    {
        owner.TF.GetComponent<BoxCollider2D>().enabled = true;
    }

   
}
