using GameEngine.State_Conditions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOutOfRange : BaseCondition
{
    private float range;

    public TargetOutOfRange(float range)
    {
        this.range = range;
    }
    public override bool FindOutcome()
    {
        if (Vector2.Distance(owner.Transform.position, owner.Target.position) > range && PlayerPhysics.Instance.IsGrounded())
            return true;
        else
            return false;
    }
}
