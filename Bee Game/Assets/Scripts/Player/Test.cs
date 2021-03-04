using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public KeyCode UpKey;
    public KeyCode DownKey=KeyCode.LeftShift;
    public KeyCode FrontSpin;
    public KeyCode BackWardSpin;
    public KeyCode LeftTurn;
    public KeyCode RightTurn;
    public KeyCode LeftSpin;
    public KeyCode RightSpin;
    float UpDownVelocity = 1f;
    public float UpDownValue;
    float UpDown;
    float yUpDown;
    //Rotate Value
    float pitch;
    float yaw;
    float UpDownTurn;
    float yUpDownTrun;
    Rigidbody rb;
    float LeftRightTurn;
    float yLeftRightTurn;
    float roll;
    float floatVelocity=0.4f;
    float LeftRightSpin;
    float yLeftRightSpin;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public enum ControlState
    {
        Mouse,
        KeyBoard
    };
    public ControlState ControlType = new ControlState();
    void FixedUpdate()
    {
        //fall slower
        if (!Input.GetKey(DownKey))
            rb.AddRelativeForce(Vector3.up * UpDownValue * floatVelocity);
        //if (EngineTurnOnOff)
        //{
        if (Input.GetKey(UpKey))
            rb.AddRelativeForce(Vector3.up * UpDownValue * UpDownVelocity);

        UpDown = KeyValue(DownKey, UpKey, UpDown, yUpDown, 1.5f, 0.01f);
        if (ControlType == ControlState.KeyBoard)
        {
            UpDownTurn = KeyValue(BackWardSpin, FrontSpin, UpDownTurn, yUpDownTrun, 1.5f, 0.1f);
            LeftRightTurn = KeyValue(LeftTurn, RightTurn, LeftRightTurn, yLeftRightTurn, 1.5f, 0.1f);
            LeftRightSpin = KeyValue(LeftSpin, RightSpin, LeftRightSpin, yLeftRightSpin, 1f, 0.1f);
        }
        //else if (ControlType == ControlState.Mouse)
        //{
        //   // Pitch Value
        //    UpDownTurn = Input.GetAxis("Mouse Y");
        //    pitch -= UpDownTurn * Time.fixedDeltaTime * 0.01f;
        //    pitch = Mathf.Clamp(Pitch, -1.2f, 1.2f);
        //   // Yaw Value
        //    LeftRightTurn = Input.GetAxis("Mouse X");
        //    yaw += LeftRightTurn * Time.fixedDeltaTime * 0.01f;
        //    LeftRightSpin = KeyValue(LeftSpin, RightSpin, LeftRightSpin, yLeftRightSpin, 1, 0.1f);
        //}
        //Pitch Value
        pitch += UpDownTurn * Time.fixedDeltaTime;
        pitch = Mathf.Clamp(pitch, -1.2f, 1.2f);
        //Yaw Value
        yaw += LeftRightTurn * Time.fixedDeltaTime;
        //roll Value
        //roll += -LeftRightSpin * Time.fixedDeltaTime;
        //roll = Mathf.Clamp(roll, -1.2f, 1.2f);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.EulerRotation(pitch, yaw,0.0f), Time.fixedDeltaTime * 1.5f);
        
    }

    void Update()
    {
        if (Input.GetKey(DownKey))
            Hover(); // hover
        else
        {
            posOffset = transform.position;
        }
            
        CheckUpDownVelocity();
    }
    float KeyValue(KeyCode A, KeyCode B, float Value, float yValue, float _float, float SmoothTime)
    {
        if (Input.GetKey(A))
            Value -= Time.deltaTime * _float;
        else if (Input.GetKey(B))
            Value += Time.deltaTime * _float;
        else
            Value = Mathf.SmoothDamp(Value, 0, ref yValue, SmoothTime);
        Value = Mathf.Clamp(Value, -1, 1);
        return Value;
    }

    void CheckUpDownVelocity()
    {
        if (UpDownVelocity > 1.0f)
            UpDownVelocity = 1.0f;
        else if (UpDownVelocity < 0.1f)
            UpDownVelocity = 0.1f;
    }
    void Hover()
    {
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
 
}
