using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 moveDirection;
    Rigidbody rb;
    PlayerController controller;
    float horizSensitivity=2f;
    float vertSensitivity=2f;
    //https://howthingsfly.si.edu/flight-dynamics/roll-pitch-and-yaw
    //for tilting side to side
    float yaw=0f;
    //for tilting up and down
    float pitch=0f;
    float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {

        //IF THE PLAYER IS MOVING 
        if (moveDirection!=Vector3.zero) // Check for movement
        {
            //SWITCH TO MOVESTATE
            controller.RequestChangeState(new MoveState(controller));
        }
        else
        {
            controller.RequestChangeState(new IdleState(controller));
        }
        controller.Move(moveDirection * Time.deltaTime, speed);
    }
    void Rotate()
    {
        //yaw += horizSensitivity * Input.GetAxis("Mouse X");
        pitch -= vertSensitivity * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        float rotation = 0;
      
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Fly");
        moveDirection.z = Input.GetAxis("Vertical");
        Rotate();
        Debug.Log(transform.eulerAngles);
    }
}
