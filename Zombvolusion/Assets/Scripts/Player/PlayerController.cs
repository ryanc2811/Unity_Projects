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
    public GameObject attackPrefab;
    Rigidbody rb;
    Vector2 movement;
        /// <summary>
        /// Units per second to move the Mover object with the joystick
        /// </summary>
    [Tooltip("Units per second to move the Mover object with the joystick")]
    private float speed;
    private bool playerDead = false;

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
        
    }
    void Start()
    {
        DontDestroyOnLoad(Mover);
        JoystickScript.JoystickExecuted = JoystickExecuted;
        rb = Mover.GetComponent<Rigidbody>();
        EventManager.instance.OnNewPlayerTrigger += NewPlayer;
        EventManager.instance.OnPlayerDeathTrigger += PlayerDead;
        speed = Mover.GetComponent<Attributes>().moveSpeed;
    }
    private void PlayerDead(GameObject player)
    {
        playerDead = true;
    }
    private void NewPlayer(GameObject player)
    {
        Mover = player;
        Mover.GetComponent<TargetEnemy>().enabled = false;
        Mover.GetComponent<PlayerFollower>().enabled = false;
        Mover.GetComponent<NavMeshAgent>().enabled = false;
        Destroy(Mover.GetComponentInChildren<MeleeAI>().gameObject);
        Instantiate(attackPrefab, Mover.transform);
        rb = Mover.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        playerDead = false;
        speed = Mover.GetComponent<Attributes>().moveSpeed;
        Mover.GetComponent<Attributes>().activePlayer = true;
    }
    private void JoystickExecuted(FingersJoystickScript script, Vector2 amount)
    {
        movement = amount;
    }
    void FixedUpdate()
    {
        if (!playerDead)
            rb.velocity=new Vector3(movement.x,0f, movement.y)*speed;
    }
    void OnDestroy()
    {
        EventManager.instance.OnNewPlayerTrigger -= NewPlayer;
        EventManager.instance.OnPlayerDeathTrigger -= PlayerDead;
    }
}
