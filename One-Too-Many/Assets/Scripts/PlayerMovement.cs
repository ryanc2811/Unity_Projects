using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //rigid body of the player
    Rigidbody rb;
    //movement direction of the player
    float moveX;
    float speed = 5f;
    float balance = 0f;
    float balanceSensitivity = 5f;
    // Start is called before the first frame update
    void Start()
    {
        //get the rigid body component from the player
        rb = GetComponent<Rigidbody>();
    }

    public void MoveLeft()
    {
        balance = Mathf.Clamp(balance += balanceSensitivity, -45f, 45f);
    }
    public void MoveRight()
    {
        balance = Mathf.Clamp(balance -= balanceSensitivity, -45f,45f);
    }
    // Update is called once per frame
    void Update()
    {
        Quaternion target= Quaternion.Euler(new Vector3(0, 0, balance));
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5f);
        //if (Application.platform == RuntimePlatform.WindowsPlayer)
        //{
        if (Input.GetMouseButtonDown(0))
            {
                MoveLeft();
                Debug.Log("afgg");
            }
            if (Input.GetMouseButtonDown(1))
            {
                MoveRight();
            }
        //}
    }
    void FixedUpdate()
    {
        //rb.velocity = new Vector3(moveX * speed, 0, 1f*speed);
    }
}
