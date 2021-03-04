using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : AI
{
    private enum State
    {
        FollowPlayer,
        ChaseTarget,
        AttackTarget,
    }
    private NavMeshAgent agent;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private GameObject player;
    private State state;
    private IAIAttack aIAttack;

    private int damage;
    private float lastAttackTime;
    private float attackDelay;
    public float attackRange = 10f;
    // Start is called before the first frame update
    void Awake()
    {
        state = State.FollowPlayer;
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<Stats>();
        targetSystem = GetComponent<TargetEnemy>();
        aIAttack = GetComponent<IAIAttack>();
        damage = (int)stats.GetValue(AttributeType.AttackDamage);
        attackDelay = stats.GetValue(AttributeType.AttackDelay);
    }
    void Start()
    {
        player = PlayerController.instance.Mover;
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
        agent.speed = stats.GetValue(AttributeType.MoveSpeed);
    }
    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(5f, 20f);
    }
    Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
    }
    // Update is called once per frame
    void Update()
    {
        target = targetSystem.currentTarget;
        switch (state)
        {
            default:
            case State.FollowPlayer:
                FollowPlayer();
                FindTarget();
                break;
            case State.ChaseTarget:
                TargetEntity();
                break;
            case State.AttackTarget:
                break;
        }
    }
    void FindTarget()
    {
        if (target)
        {
            float targetRange = 20f;
            if (Vector3.Distance(transform.position, target.transform.position) < targetRange)
            {
                state = State.ChaseTarget;
            }
        }
    }
    void FollowPlayer()
    {
        //Debug.Log("Roaming");
        agent.isStopped = false;
        if (Vector3.Distance(transform.position, player.transform.position) > 15f)
            agent.SetDestination(player.transform.position);
    }
    void TargetEntity()
    {
        if (target)
        {
            agent.SetDestination(target.transform.position);
            transform.LookAt(target.transform);
            //Debug.Log("Attacking");
            if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
            {
                //Enemy in range
                agent.isStopped = true;
                if (Time.time > lastAttackTime)
                {
                    lastAttackTime = Time.time + attackDelay;
                    //state = State.AttackTarget;
                    //Shoot
                    aIAttack.Attack(target, damage);
                }
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            state = State.FollowPlayer;
        }
    }
}
