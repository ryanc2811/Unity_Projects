using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    //Keys
    public KeyCode UpKey = KeyCode.Space;
    public KeyCode DownKey = KeyCode.LeftShift;
    public KeyCode Forward = KeyCode.W;
    public KeyCode BackWard = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode LeftSpin = KeyCode.Q;
    public KeyCode RightSpin = KeyCode.E;
    //Velocity
    float Velocity = 1f;
    //Quanterion
    Quaternion Rotation;

    // Start is called before the first frame update
    public void Start()
    {
       // Rotation = GetComponent();  
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        
    }
}
