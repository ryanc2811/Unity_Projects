using UnityEngine;

public class AttackState : IState,IAttackState
{
    private Transform target;
    private Transform attacker;
    private LineRenderer line;
    private float lastAttack;
    private float attackDelay = .6f;
    public void Begin()
    {
        Debug.Log("Attack State");
        line = attacker.GetComponent<LineRenderer>();
        lastAttack = Time.time;
    }

    public void End()
    {
        line.enabled = false;
    }

    public void Execute()
    {
        if (target)
        {
            //attack
            line.enabled = true;
            line.positionCount = 2;
            line.SetPosition(0, attacker.position);
            line.SetPosition(1, target.position);
            if (Time.time > lastAttack + attackDelay)
            {
                lastAttack = Time.time;
                EventManager.instance.BulletDestroyedTrigger();

            }
        }
    }

    public void SetCurrentTransform(Transform attacker)
    {
        this.attacker = attacker;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
