using GameEngine.BehaviourManagement.StateMachine_Stuff;
using GameEngine.Collision;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnemyStateMachine :StateMachine,ICollidable
{


    /// <summary>
    /// Initialises the statemachine
    /// </summary>
    public override void Initialise()
    {
        base.Initialise();
        OnStateChanged += CharacterStateMachine_OnStateChanged;
    }
    /// <summary>
    /// Sets the current state index once state has changed
    /// </summary>
    /// <param name="pStateIndex"></param>
    private void CharacterStateMachine_OnStateChanged(int pStateIndex) => currentStateIndex = pStateIndex;


    #region ICollidable
    public void OnCollisionEnter(Collision2D other)
    {
        if (currentState is IStateWithCollision)
            ((IStateWithCollision)currentState).OnCollisionEnter(other);
    }

    public void OnCollisionExit(Collision2D other)
    {
        if (currentState is IStateWithCollision)
            ((IStateWithCollision)currentState).OnCollisionExit(other);
    }

    public void OnCollisionStay(Collision2D other)
    {
        if (currentState is IStateWithCollision)
            ((IStateWithCollision)currentState).OnCollisionStay(other);
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (currentState is IStateWithCollision)
            ((IStateWithCollision)currentState).OnTriggerEnter(other);
    }

    public void OnTriggerExit(Collider2D other)
    {
        if (currentState is IStateWithCollision)
            ((IStateWithCollision)currentState).OnTriggerExit(other);
    }

    #endregion

}
