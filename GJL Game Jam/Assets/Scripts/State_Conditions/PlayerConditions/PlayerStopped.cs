using GameEngine.State_Conditions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopped : BaseCondition
{
    bool stopped = false;

    public override bool FindOutcome()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            return false;

        return true;
    }
}
