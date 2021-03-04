using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private State currentState;

    private bool attacking = false;

    private float attackDelay = 3f;
    private float lastAttack;

    private bool isStunned = false;

    [SerializeField]
    Material defaultMaterial;
    [SerializeField]
    Material stunnedMaterial;
    [SerializeField]
    private AudioSource attackSound;
    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    private Transform lastVisibleTarget;
    private bool eventLaunched;

    private bool targetFound = false;
    public LayerMask obstacleMask;
    public LayerMask targetMask;

    private Vector3 startPosition;

    [SerializeField]
    private AIPath path;

    [SerializeField]
    public float viewRadius;
    [SerializeField]
    [Range(0, 360)]
    public float viewAngle;
    private enum State
    {
        Stay,
        MoveToTarget,
        MoveToStart,
        Attack,
        Stunned
    }

    private void Awake()
    {
        currentState = State.Stay;
        path = GetComponent<AIPath>();
        startPosition = transform.position;
    }
    IEnumerator StartAttack()
    {
        attacking = true;
        //Play Attack Sound
        attackSound.Play();
        yield return new WaitForSeconds(2f);
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            default:
            case State.Stay:
                Stay();
                LookForTarget();
                break;
            case State.MoveToStart:
                MoveToStart();
                LookForTarget();
                break;
            case State.MoveToTarget:
                MoveToTarget();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Stunned:
                StunEnemy();
                break;

        }


        if (attacking)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentState = State.Stunned;
            }
        }
    }

    private void StunEnemy()
    {
        StartCoroutine(Stun());
    }
    private IEnumerator Stun()
    {
        isStunned = true;
        GetComponent<Renderer>().material = stunnedMaterial;
        yield return new WaitForSeconds(3f);
        isStunned = false;
        GetComponent<Renderer>().material = defaultMaterial;
        lastAttack = Time.time;
        currentState = State.Attack;
    }
    private void MoveToStart()
    {
        //Debug.Log("MoveToStart");
        path.destination = startPosition;
        if (Vector2.Distance(transform.position, startPosition) < 1f)
        {
            currentState = State.Stay;
        }
    }

    private void MoveToTarget()
    {
        Transform target = lastVisibleTarget;
        if (target)
        {
            path.destination = (Vector2)target.position;
            
            if (CloseToTarget())
                currentState = State.Attack;
            //Debug.Log("MoveToTarget");
            float stopChasingDistance = 15f;
            if (Vector2.Distance(transform.position, target.position) > stopChasingDistance)
            {
                //Debug.Log("MoveToStart");
                currentState = State.MoveToStart;
            }
        }
        else
            currentState = State.Stay;
    }

    private void LookForTarget()
    {
        FindVisibleTargets();
        if (targetFound)
            currentState = State.MoveToTarget;
    }
    private void Stay()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    private bool CloseToTarget()
    {
        float reachedPositionDistance = 3f;
        if (Vector2.Distance(transform.position, lastVisibleTarget.position) < reachedPositionDistance)
        {
            return true;
        }
        return false;
    }
    private void Attack()
    {
        Stay();
        if (!CloseToTarget())
            currentState = State.MoveToTarget;
        
        if (!attacking&&CloseToTarget())
        {
            if (Time.time > lastAttack + attackDelay)
            {
                StartCoroutine(StartAttack());
                lastAttack = Time.time;
                //Debug.Log("Attack");
            }
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            if (Vector2.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);
                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    targetFound = true;
                    lastVisibleTarget = target;
                    eventLaunched = false;
                }
            }
            else
            {
                if (!eventLaunched)
                {
                    targetFound = false;
                    lastVisibleTarget = null;
                    eventLaunched = true;
                }
            }
        }
        if (targetsInViewRadius.Length == 0 && !eventLaunched)
        {
            targetFound = false;
            lastVisibleTarget = null;
            eventLaunched = true;
        }
    }


    public Vector3 DirFromAngles(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        float angleRad = angleInDegrees * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
