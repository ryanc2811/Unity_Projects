using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    float rotation;
    float maximumSpeed = 50f;
    float boostSpeed = 20f;
    public bool Boosted { get; private set; }
    private bool canMoveLeft = false;
    private bool canMoveRight = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotation = transform.localEulerAngles.z;
    }

    public void MoveLeft(bool holding)
    {
        canMoveLeft = holding;
    }
    public void MoveRight(bool holding)
    {
        canMoveRight = holding;
    }
    void Move()
    {
        if (Input.GetKey(KeyCode.A))
            rotation--;
        if (Input.GetKey(KeyCode.D))
            rotation++;

        if (canMoveLeft)
            rotation--;
        if (canMoveRight)
            rotation++;

        rb.MoveRotation(Quaternion.Euler(new Vector3(0f, 0f, rotation)));
    }
    // Update is called once per frame
    void Update()
    {
        Move();

        float speed = Vector3.Magnitude(rb.velocity);  // test current object speed
        if (speed < boostSpeed)
            Boosted = true;
        else
            Boosted = false;

        if (speed > maximumSpeed)
        {
            float brakeSpeed = speed - maximumSpeed;  // calculate the speed decrease

            Vector3 normalisedVelocity = rb.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

            rb.AddForce(-brakeVelocity);  // apply opposing brake force
        }
    }

    
}
