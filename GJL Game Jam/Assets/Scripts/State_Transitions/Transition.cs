using GameEngine.BehaviourManagement.StateMachine_Stuff;
using GameEngine.State_Conditions;
using GameEngine.Entities;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GameEngine.Transitions
{
    /// <summary>
    /// Transitions to a new state when a condition returns true
    /// </summary>
    class Transition : ITransition
    {
        //DECLARE an ICondition for storing the condition of the transition
        private ICondition condition;
        //DECLARE an int for storing the key of the transition state
        private int transitionState;
        //DECLARE an int for storing the key of the current state
        private int currentState;
        //DECLARE a double for storing the delay of the transition
        private float delay = 0f;
        //DECLARE a TimeSpan for storing the elapsed time
        private float timeRemaining;
        //DECLARE a string for storing the name of the transition
        private string transitionName;

        /// <summary>
        /// Intitialises all the members
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pCondition"></param>
        /// <param name="pTransitionState"></param>
        /// <param name="pCurrentState"></param>
        /// <param name="pDelay"></param>
        public Transition(string pName,ICondition pCondition, int pTransitionState, int pCurrentState, float pDelay)
        {
            transitionName = pName;
            condition = pCondition;
            transitionState = pTransitionState;
            currentState = pCurrentState;
            delay = pDelay;
        }

        //returns the condition
        public ICondition Condition => condition;

        public string Name => transitionName;

        /// <summary>
        /// Execute the transition depending on the outcome of the condition
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns>The next state to transition to</returns>
        public int ExecuteTransition()
        {
            //if the condition is true
            if (condition.FindOutcome())
            {

                //if the delay is less than or equal to 0, then return the transition state straight away
                if (delay <= 0)
                    return transitionState;
                return FinishTransition();
            }
            else
                return currentState;
        }
        /// <summary>
        /// Starts the delay of the transition
        /// </summary>
        /// <param name="elapsedGameTime"></param>
        /// <returns>the transition state once the delay has be enacted</returns>
        private int FinishTransition()
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                return currentState;
            }
            else
                return transitionState;
        }
        /// <summary>
        /// Close the transition and the condition
        /// </summary>
        public void ExitTransition()
        {
            condition.ExitCondition();
        }
        /// <summary>
        /// Passes the AIUser to the condition
        /// </summary>
        /// <param name="owner"></param>
        public void SetOwner(IAIUser owner)
        {
            condition.SetOwner(owner);
        }
        /// <summary>
        /// Starts the transition and condition
        /// </summary>
        public void StartTransition()
        {
            condition.StartCondition();
            timeRemaining = delay;
            //Debug.Log((PlayerStates)transitionState + " " + (PlayerStates)currentState);
        }
    }
}
