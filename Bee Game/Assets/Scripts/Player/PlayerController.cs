using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region DataMembers
    // How much to smooth out the movement
    [Range(0, .3f)] [SerializeField] public float m_MovementSmoothing = .05f;
    //DECLARE a Statmachine for storing a reference to the state machine and switch between each state of the player, call it stateMachine
    private StateMachine stateMachine;
    //DECLARE a float property for getting and setting the players movement speed, call it Speed
    public float Speed { get; set; }
    //DECLARE a Animator
    public Animator animator;
    //DECLARE a Vector2 for direction of the player
    public Vector3 direction { get; set; }
    //DECLARE a Rigidbody of the player
    public Rigidbody RB { get; set; }
    //DECLARE a Transform of the player
    public Transform TF { get; set; }
    #endregion


    private void Awake()
    {
        //Get necessary components from parent object
        RB = GetComponent<Rigidbody>();
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
    public void Move(Vector3 moveVector, float speed)
    {
        direction = moveVector;
        Speed = speed;
    }
    /// <summary>
    ///Update function for updating the character controller 
    /// </summary>
    public void FixedUpdate()
    {
        //update the state machine
        stateMachine.Update();
    }

}