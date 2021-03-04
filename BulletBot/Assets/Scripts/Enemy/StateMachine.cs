using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine:IStateMachine
{
    private IState currentState;

    public IState GetCurrentState()
    {
        return currentState;
    }
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.End();
        }
        currentState = newState;
        currentState.Begin();
    }
    // Update is called once per frame
    public void Update()
    {
        if(currentState!=null)
            currentState.Execute();
    }
}
