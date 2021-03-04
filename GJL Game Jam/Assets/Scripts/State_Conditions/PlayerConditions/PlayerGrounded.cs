using GameEngine.State_Conditions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : BaseCondition
{
    bool grounded = false;

    public override bool FindOutcome()
    {
        grounded = PlayerPhysics.Instance.IsGrounded();
        return grounded;
    }

    public override void ExitCondition()
    {
        grounded=false;
    }
}
