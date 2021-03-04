using GameEngine.State_Conditions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInRange : BaseCondition
{
    private float range;

    private Transform closestTarget;
    public TargetInRange(float range)
    {
        this.range = range;
    }
    public override bool FindOutcome()
    {
        if (Vector2.Distance(owner.Transform.position, owner.Target.position) < range)
            return true;
        else
            return false;
    }
}
