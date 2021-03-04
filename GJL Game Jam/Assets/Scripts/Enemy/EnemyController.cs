using GameEngine.BehaviourManagement;
using GameEngine.BehaviourManagement.StateMachine_Stuff;
using GameEngine.Collision;
using GameEngine.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IAIUser
{
    private Rigidbody2D rb;
    public Transform Transform => transform;
    protected IStateMachine stateMachine;
    public Rigidbody2D RB => rb;
    [SerializeField]
    private Animator animator;
    public Animator Anim => animator;

    private Transform target;
    public Transform Target => target;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        InitialiseStateMachine();
    }
    protected virtual void InitialiseStateMachine() { }

    #region Collision
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (stateMachine is ICollidable)
        {
            ((ICollidable)stateMachine).OnTriggerEnter(other);
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (stateMachine is ICollidable)
        {
            ((ICollidable)stateMachine).OnTriggerExit(other);
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (stateMachine is ICollidable)
        {
            ((ICollidable)stateMachine).OnCollisionEnter(other);
        }
    }
    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        if (stateMachine is ICollidable)
        {
            ((ICollidable)stateMachine).OnCollisionExit(other);
        }
    }
    protected virtual void OnCollisionStay2D(Collision2D other)
    {
        if (stateMachine is ICollidable)
        {
            ((ICollidable)stateMachine).OnCollisionStay(other);
        }
    }

    #endregion
    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
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
}
