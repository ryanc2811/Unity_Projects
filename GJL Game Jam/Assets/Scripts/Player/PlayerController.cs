using GameEngine.BehaviourManagement;
using GameEngine.BehaviourManagement.StateMachine_Stuff;
using GameEngine.Collision;
using GameEngine.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IAIUser
{
    IStateMachine stateMachine;
    private Rigidbody2D rb;
    public Transform Transform => transform;
    
    public Rigidbody2D RB => rb;
    [SerializeField]
    private Animator animator;
    public Animator Anim => animator;
    private SpriteRenderer spriteRenderer;
    public Transform Target => throw new System.NotImplementedException();

    public Transform GroundDetection =>transform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stateMachine = new PlayerStateMachine();
        ((IAIComponent)stateMachine).SetAIUser(this);
        IStateFactory stateFactory = new StateFactory();
        stateMachine.AddState((int)PlayerStates.Idle, stateFactory.Create<PlayerIdleState>());
        stateMachine.AddState((int)PlayerStates.Moving, stateFactory.Create<PlayerMoveState>());
        stateMachine.AddState((int)PlayerStates.Jump, stateFactory.Create<PlayerJumpState>());
        ((IAIComponent)stateMachine).Initialise();
        animator.SetBool("PistolEquipped", true);
    }
    #region Collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if(stateMachine is ICollidable)
        {
            ((ICollidable)stateMachine).OnTriggerEnter(other);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (stateMachine is ICollidable)
        {
            ((ICollidable)stateMachine).OnTriggerExit(other);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (stateMachine is ICollidable)
        {
            ((ICollidable)stateMachine).OnCollisionEnter(other);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (stateMachine is ICollidable)
        {
            ((ICollidable)stateMachine).OnCollisionExit(other);
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (stateMachine is ICollidable)
        {
            ((ICollidable)stateMachine).OnCollisionStay(other);
        }
    }
    #endregion
    
    void LateUpdate()
    {
        stateMachine.LateUpdate();
    }
    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
    public void SetPosition(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }

    public void SetVelocity(float x, float y)
    {
       rb.velocity = new Vector2(x, y);
    }
    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        

    }
}
