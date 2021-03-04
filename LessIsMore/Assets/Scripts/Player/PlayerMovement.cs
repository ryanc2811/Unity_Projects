using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
 
  
    public float moveSpeed = 40f;
    float moveLimiter = 0.7f;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //SET MOVEMENT VECTORS WITH KEY PRESSES
        movement.x = Input.GetAxisRaw("Horizontal")*moveSpeed;
        movement.y = Input.GetAxisRaw("Vertical") * moveSpeed;
        
    }
    
    
    void FixedUpdate()
    {
        if (movement.x != 0 && movement.y != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            movement.x *= moveLimiter;
            movement.y *= moveLimiter;
        }
       
        //IF THE PLAYER IS MOVING 
        if(movement.x != 0 || movement.y != 0) // Check for movement
        {
            //SWITCH TO MOVESTATE
            controller.RequestChangeState(new MoveState(controller));
        }
        else if(movement.x== 0 && movement.y == 0)
        {
            //SWITCH TO IDLE STATE
            controller.RequestChangeState(new IdleState(controller));
        }
        controller.Move(movement * Time.fixedDeltaTime,moveSpeed);
    }
}
