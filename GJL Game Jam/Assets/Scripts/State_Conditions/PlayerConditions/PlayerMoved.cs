using GameEngine.State_Conditions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoved : BaseCondition
{
    /// <summary>
    /// Return the outcome of the condition
    /// </summary>
    /// <returns></returns>
    public override bool FindOutcome()
    {
        if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
            return true;
        
        return false;
    }
}
