using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState currentState;
    /// <summary>
    /// CHANGE THE CURRENT STATE FOR A NEWS STATE
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeCurrentState(IState newState)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    // Update is called once per frame
    public void Update()
    {
        //EXECUTE THE CURRENT STATE
        if (currentState != null)
            currentState.Execute();
        //Debug.Log(currentState);
       
    }
}
