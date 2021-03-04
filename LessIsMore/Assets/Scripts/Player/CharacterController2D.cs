using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterController2D : MonoBehaviour, IController
{
    #region DataMembers
    // How much to smooth out the movement
    [Range(0, .3f)] [SerializeField] public float m_MovementSmoothing = .05f;  
    //DECLARE a Vector3 for storing the players velocity, call it, m_Velocity
    public Vector3 m_Velocity = Vector3.zero;
    //DECLARE a Statmachine for storing a reference to the state machine and switch between each state of the player, call it stateMachine
    private StateMachine stateMachine;
    //DECLARE a float property for getting and setting the players movement speed, call it Speed
    public float Speed { get; set; }
    //DECLARE a Animator
    public Animator animator;
    //DECLARE a Vector2 for movement of the player
    public Vector2 movement { get; set; }
    //DECLARE a Vector2 for direction of the player
    public Vector2 direction { get; set; }
    //DECLARE a Rigidbody of the player
    public Rigidbody2D RB { get; set; }
    //DECLARE a Transform of the player
    public Transform TF { get ; set ; }
    //DECLARE a float for the cooldown of the dash
    private float dashTimer;
    //DECLARE a float for the cooldown rate of the dash
    private float dashCooldown = 2f;
    //DECLARE a GameObject for the dashEffect
    public GameObject DashEffect;
    #endregion


    private void Awake()
    {
        //Get necessary components from parent object
        RB = GetComponent<Rigidbody2D>();
        TF = GetComponent<Transform>();

    }


    public void Start()
    {
        //INSTANTIATE a new StateMachine
        stateMachine = new StateMachine();
        
    }

    

    /// <summary>
    /// A method for requesting the state to change to the given parameter
    /// </summary>
    /// <param name="requestedState"></param>
    public void RequestChangeState(IState requestedState)
    {
        //change state
        stateMachine.ChangeCurrentState(requestedState);
    }
    /// <summary>
    /// a setter for the player movement class to set their movement variable and speed variable to local variables
    /// </summary>
    /// <param name="moveVector"></param>
    /// <param name="speed"></param>
    public void Move(Vector2 moveVector, float speed)
    {
        movement = moveVector;
        Speed = speed;
    }
    /// <summary>
    ///Update function for updating the character controller 
    /// </summary>
    public void Update()
    {

        //if (UIHandler.Instance.IsMouseOverIgnoredUI())
        //    return;
        //check if the dash timer hasn't reached 0
        if (dashTimer >= 0f)
        {
            //count down by time.delta time
            dashTimer -= Time.deltaTime;
        }
        //if it is less than 0(cooldown)
        if (dashTimer < 0f)
        {
            //if the player presses space key and is not moving
            if (Input.GetKeyDown(KeyCode.Space) && movement != new Vector2(0, 0))
            {
                //INSTANTIATE a particle effect ath the players position
                Instantiate(DashEffect, transform.position, Quaternion.identity);
                //change state to dash state
                RequestChangeState(new DashState(this));
                //set the timer back to the beginning of the countdown
                dashTimer = dashCooldown;
            }

        }
        //update the state machine
        stateMachine.Update();

    }
     
}

