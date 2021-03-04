using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
using UnityEngine.AI;

/// <summary>
/// Player Controller script
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Fingers joystick script
    /// </summary>
    [Tooltip("Fingers Joystick Script")]
    public FingersJoystickScript JoystickScript;
        /// <summary>
        /// Object to move with the joystick
        /// </summary>
    [Tooltip("Object to move with the joystick")]
    public GameObject Mover;
    Rigidbody rb;
    Vector2 movement;
        /// <summary>
        /// Units per second to move the Mover object with the joystick
        /// </summary>
    [Tooltip("Units per second to move the Mover object with the joystick")]
    private float speed;
    private bool stopMoving = false;
    public static PlayerController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
        DontDestroyOnLoad(gameObject);
        JoystickScript.JoystickExecuted = JoystickExecuted;

    }
    void Start()
    {
        //Mover = GameObject.FindGameObjectWithTag("Player");
        
        rb = Mover.GetComponent<Rigidbody>();
        speed = Mover.GetComponent<Stats>().GetValue(AttributeType.MoveSpeed);
    }
    public bool isMoving()
    {
        return movement != Vector2.zero;
    }
    public void StopMoving(bool pStopmoving)
    {
        stopMoving = pStopmoving;
    }
    private void JoystickExecuted(FingersJoystickScript script, Vector2 amount)
    {
        if (stopMoving)
            return;
        movement = amount;
    }
    void Update()
    {
        if (Mover)
        {
            if (stopMoving)
                movement = Vector2.zero;
            speed = Mover.GetComponent<Stats>().GetValue(AttributeType.MoveSpeed);
            if (isMoving()&&!GameState.instance.paused)
            {
                Mover.transform.rotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.y));
            }
        }
    }
    void FixedUpdate()
    {
        if (!GameState.instance.paused)
            rb.velocity=new Vector3(movement.x,0f, movement.y)*speed;
    }
}
