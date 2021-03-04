using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine
{
    IState CurrentState { get; }
    void ChangeCurrentState(IState newState);
    // Update is called once per frame
    void Update();
}
