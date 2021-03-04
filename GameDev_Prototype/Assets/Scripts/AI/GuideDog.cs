using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuideDog : MonoBehaviour
{
    State currentState;

    public LayerMask obstacleMask;
    public LayerMask targetMask;

    private List<Transform> visitedPlaces;


    private Vector3 searchPosition;

    [SerializeField]
    private SoundLineSpawner lineSpawner;
    [SerializeField]
    AIPath path;
    [SerializeField]
    public float viewRadius;
    [SerializeField]
    [Range(0, 360)]
    public float viewAngle;

    private float lastTimeBarked;
    [SerializeField]
    private float barkDelay;

    [SerializeField]
    Transform playerTransform;

    Vector2 roamPosition;

    [SerializeField] AudioSource barkSound;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    private Transform lastVisibleTarget;
    private bool eventLaunched;

    private bool targetFound = false;
    private enum State
    {
        Roam,
        MovingTowardsTarget,
        Stay,
        MovingBackToPlayer
    }
    void Awake()
    {
        currentState = State.Roam;
        path = GetComponent<AIPath>();
        visitedPlaces = new List<Transform>();
    }
    void Start()
    {
        GetRoamingPosition();
    }

    IEnumerator FindValidPosition()
    {
        while (true)
        {
            searchPosition = (Vector2)playerTransform.position + GetRandomDir() * UnityEngine.Random.Range(5f, 40f);
            if (TileGrid.Instance.IsPositionTraversable(searchPosition))
            {
                roamPosition = searchPosition;
                yield break;
            }
            yield return null;
        }
    }

    private void GetRoamingPosition()
    {
        StartCoroutine("FindValidPosition");
    }
    Vector2 GetRandomDir()
    {
        return new Vector2(UnityEngine.Random.Range(-1f, 1f),UnityEngine.Random.Range(-1f, 1f));
    }
    void StopMoving()
    {
        //characterController.StopMoving();
    }

    private void Update()
    {
        switch (currentState)
        {
            default:
            case State.Roam:
                Roam();
                LookForTarget();
                break;
            case State.MovingTowardsTarget:
                MoveToTarget();
                break;
            case State.Stay:
                StopAndBark();
                break;
            case State.MovingBackToPlayer:
                MoveToPlayer();
                break;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Bark();
    }

    private void MoveToPlayer()
    {
        path.destination=playerTransform.position;
        Debug.Log("Moving To Start");
        if(CloseToPlayer())
            currentState = State.Roam;
    }

    private void Bark()
    {
        barkSound.Play();
        lineSpawner.SpawnLines();
    }
    private void StopAndBark()
    {
        StopMoving();
        if (Time.time > lastTimeBarked + barkDelay)
        {
            lastTimeBarked = Time.time;
            barkDelay = UnityEngine.Random.Range(0.2f, 5f);
            Bark();
        }
        if (CloseToPlayer())
        {
            currentState = State.Roam;
            if (!visitedPlaces.Contains(lastVisibleTarget))
                visitedPlaces.Add(lastVisibleTarget);
        }
            
    }

    private bool CloseToPlayer()
    {
        float reachedPositionDistance = 5f;
        if (Vector2.Distance(transform.position, playerTransform.position) < reachedPositionDistance)
        {
            return true;
        }
        return false;
    }
    void Roam()
    {
        path.destination=roamPosition;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position,1f, Vector2.up, 0.5f, obstacleMask);

        if (hit.collider)
        {
            GetRoamingPosition();
        }

        //Debug.Log(Vector3.Distance(transform.position, roamPosition));
        if (Vector3.Distance(transform.position, roamPosition) < 1f)
        {
            //Debug.Log("Roaming");
            GetRoamingPosition();
        }
    }
    private void MoveToTarget()
    {
        Transform target = lastVisibleTarget;
        if (target&&!visitedPlaces.Contains(target))
        {
            path.destination=(Vector2)target.position;

            if (Vector2.Distance(transform.position, target.position) < 1f)
                currentState = State.Stay;

            float stopChasingDistance = 35f;
            if (Vector2.Distance(transform.position, target.position) > stopChasingDistance)
            {
                currentState = State.MovingBackToPlayer;
            }
        }
        else
            currentState = State.Roam;
    }

    private void LookForTarget()
    {
        FindVisibleTargets();
        if (targetFound)
            currentState = State.MovingTowardsTarget;
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


