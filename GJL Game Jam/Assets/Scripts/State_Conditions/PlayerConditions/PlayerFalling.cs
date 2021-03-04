using GameEngine.State_Conditions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : BaseCondition
{
    public override bool FindOutcome()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return false;
        }
        return true;
    }
}
