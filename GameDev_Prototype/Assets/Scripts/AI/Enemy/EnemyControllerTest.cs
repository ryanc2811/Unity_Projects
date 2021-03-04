using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerTest : MonoBehaviour
{
    private Rigidbody2D rb;

    private State currentState;

    private bool attacking = false;

    private float attackDelay = 4f;
    private float lastAttack;
    private float attackRange = 6f;

    private float speed = 5f;

    private bool startPositionSelected = false;
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private AudioSource attackSound;
    //[HideInInspector]
    //public List<Transform> visibleTargets = new List<Transform>();

    //private Transform lastVisibleTarget;
    //private bool eventLaunched;

    //private bool targetFound = false;
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
    private Vector3 searchPosition;

    private enum State
    {
        Stay,
        MoveToStart,
        Attack
    }

    private void Awake()
    {
        currentState = State.Stay;
        startPosition = transform.position;
        rb=GetComponent<Rigidbody2D>();
        path = GetComponent<AIPath>();
    }

    private void GetStartingPosition()
    {
        StartCoroutine("FindValidPosition");
    }

    IEnumerator FindValidPosition()
    {
        while (true)
        {
            searchPosition = transform.position + GetRandomDir() * 3f;
            if (TileGrid.Instance.IsPositionTraversable(searchPosition))
            {
                startPosition = searchPosition;
                startPositionSelected = true;
                yield break;
            }
            yield return null;
        }
    }
    Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
    }
    IEnumerator StartAttack()
    {
        attacking = true;
        //Play Attack Sound
        attackSound.Play();
        yield return new WaitForSeconds(2f);
        attacking = false;
        currentState = State.MoveToStart;
    }

    // Update is called once per frame
    void FixedUpdate()
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
                break;
            case State.Attack:
                Attack();
                break;

        }
    }

    private void MoveToStart()
    {
        Debug.Log("MoveToStart");

        path.maxSpeed =speed;

        path.destination = startPosition;

        if (Vector2.Distance(transform.position, startPosition) < 1f)
        {
            currentState = State.Stay;
        }
    }

    private void LookForTarget()
    {
        //FindVisibleTargets();
        if (playerTransform)
            if(CloseToPlayer())
                currentState = State.Attack;
    }
    private void Stay()
    {
        Debug.Log("Stay");
        rb.velocity = Vector2.zero;
    }
    private bool CloseToPlayer()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < attackRange)
        {
            return true;
        }
        return false;
    }
    //private bool CloseToTarget()
    //{
    //    float reachedPositionDistance = 3f;
    //    if (Vector2.Distance(transform.position, lastVisibleTarget.position) < reachedPositionDistance)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    //private bool InAttackRange()
    //{
    //    if (Vector2.Distance(transform.position, lastVisibleTarget.position) < attackRange)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
    private void Attack()
    {

        if (!attacking)
        {
            if (Time.time > lastAttack + attackDelay)
            {
                StartCoroutine(StartAttack());
                lastAttack = Time.time;
                Debug.Log("Attack");
                path.maxSpeed = 10f;
                path.destination = playerTransform.position;    
                GetStartingPosition();
            }
        }
    }

    //void FindVisibleTargets()
    //{
    //    visibleTargets.Clear();
    //    Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

    //    for (int i = 0; i < targetsInViewRadius.Length; i++)
    //    {
    //        Transform target = targetsInViewRadius[i].transform;
    //        Vector2 directionToTarget = (target.position - transform.position).normalized;
    //        if (Vector2.Angle(transform.forward, directionToTarget) < viewAngle / 2)
    //        {
    //            float distanceToTarget = Vector2.Distance(transform.position, target.position);
    //            if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
    //            {
    //                visibleTargets.Add(target);
    //                targetFound = true;
    //                lastVisibleTarget = target;
    //                eventLaunched = false;
    //            }
    //        }
    //        else
    //        {
    //            if (!eventLaunched)
    //            {
    //                targetFound = false;
    //                lastVisibleTarget = null;
    //                eventLaunched = true;
    //            }
    //        }
    //    }
    //    if (targetsInViewRadius.Length == 0 && !eventLaunched)
    //    {
    //        targetFound = false;
    //        lastVisibleTarget = null;
    //        eventLaunched = true;
    //    }
    //}


    //public Vector3 DirFromAngles(float angleInDegrees, bool angleIsGlobal)
    //{
    //    if (!angleIsGlobal)
    //    {
    //        angleInDegrees += transform.eulerAngles.z;
    //    }
    //    float angleRad = angleInDegrees * (Mathf.PI / 180f);
    //    return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    //}
}
