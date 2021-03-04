using GameEngine.BehaviourManagement.StateMachine_Stuff;
using GameEngine.Commands;
using GameEngine.Transitions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerIdleState : BaseState,IStateWithCollision
{
    public Action<Collision2D> OnCollisionEnterEvent { get; set; }
    public Action<Collision2D> OnCollisionExitEvent { get; set; }
    public Action<Collision2D> OnCollisionStayEvent { get; set; }
    public Action<Collider2D> OnTriggerEnterEvent { get; set; }
    public Action<Collider2D> OnTriggerExitEvent { get; set; }

    #region IStateWithCollision
    public void OnCollisionEnter(Collision2D other)
    {
        OnCollisionEnterEvent?.Invoke(other);
    }

    public void OnCollisionExit(Collision2D other)
    {
        OnCollisionExitEvent?.Invoke(other);
    }

    public void OnCollisionStay(Collision2D other)
    {
        OnCollisionStayEvent?.Invoke(other);
    }

    public void OnTriggerEnter(Collider2D other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }

    public void OnTriggerExit(Collider2D other)
    {
        OnTriggerExitEvent?.Invoke(other);
    }

    #endregion

    /// <summary>
    /// Sets all the commands in the commands array
    /// </summary>
    public override void SetCommands()
    {
        commands = new BaseCommand[]
        {
            new PlayerIdleCommand(),
        };
    }
    /// <summary>
    /// Sets all the transitions in the transitions array
    /// </summary>
    public override void SetTransitions()
    {
        transitions = new List<ITransition>()
        {
            new Transition("ToJump",new PlayerJumped(),(int)PlayerStates.Jump, StateIndex(),0),
            new Transition("ToMove",new PlayerMoved(),(int)PlayerStates.Moving, StateIndex(),0)
        };
    }
    /// <summary>
    /// Returns the character state as an integer
    /// </summary>
    /// <returns></returns>
    public override int StateIndex()
    {
        return (int)PlayerStates.Idle;
    }
}
