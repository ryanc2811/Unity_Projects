using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    // Called once when the state has been entered
    void Enter();
    // Update the state
    void Execute();

    //Exit the state
    void Exit();
}
