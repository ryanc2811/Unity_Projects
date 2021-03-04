using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : Enemy
{
    private FieldOfView fov;
    protected override void Initialize()
    {
        fov = GetComponent<FieldOfView>();
        ((TargetFinder)fov).OnNewTargetFoundCallback += AttackTarget;
        ((TargetFinder)fov).OnTargetNotFoundCallback += TargetNotFound;
        
    }
    private void TargetNotFound()
    {
        IState idleState = new IdleState();
        controller.ChangeState(idleState);
    }
    private void AttackTarget(Transform target)
    {
        IAttackState attackState = new AttackState();
        attackState.SetTarget(target);
        attackState.SetCurrentTransform(transform);
        controller.ChangeState((IState)attackState);
    }
    void OnDestroy()
    {
        ((TargetFinder)fov).OnNewTargetFoundCallback -= AttackTarget;
        ((TargetFinder)fov).OnTargetNotFoundCallback -= TargetNotFound;
    }
}
