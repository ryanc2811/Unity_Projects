using GameEngine.Collision;
using GameEngine.Commands;
using GameEngine.Entities;
using GameEngine.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameEngine.BehaviourManagement.StateMachine_Stuff
{
    public abstract class BaseState:IState
    {
        //DECLARE an ICommand[] for storing a collection of command that relate to the current state
        protected ICommand[] commands;
        //DECLARE an ITransition[] for storing a collection of transtions that relate to the current state
        protected IList<ITransition> transitions;
        //DECLARE an IStatemachine for storing a reference to the state's statemachine
        protected IStateMachine stateMachine;
        protected IAIUser owner;
        int state;
        /// <summary>
        /// Initialises the state
        /// </summary>
        /// <param name="pStateMachine"></param>
        /// <param name="owner"></param>
        public void InitState(IStateMachine pStateMachine, IAIUser owner)
        {
            //Set the statemachine member
            stateMachine = pStateMachine;
            //Populate the commands array
            SetCommands();
            //Populate the transitions array
            SetTransitions();
            //Pass reference to IAIUser to transitions and commands
            SetOwner(owner);
            if (this is IStateWithCollision)
                SubscribeCollisionListeners();
        }

        private void SubscribeCollisionListeners()
        {
            foreach (ITransition transition in transitions)
            {
                //If the condition in transition uses collision, then subscribe the method to the collision event
                if (transition.Condition is ICollidable)
                {
                    ((IStateWithCollision)this).OnCollisionEnterEvent += ((ICollidable)transition.Condition).OnCollisionEnter;
                    ((IStateWithCollision)this).OnCollisionExitEvent += ((ICollidable)transition.Condition).OnCollisionExit;
                    ((IStateWithCollision)this).OnCollisionStayEvent += ((ICollidable)transition.Condition).OnCollisionStay;
                }
                    
            }
            foreach (ICommand command in commands)
            {
                //if the command uses collision, then subscribe the method to the input event
                if (command is ICollidable)
                {
                    ((IStateWithCollision)this).OnCollisionEnterEvent += ((ICollidable)command).OnCollisionEnter;
                    ((IStateWithCollision)this).OnCollisionExitEvent += ((ICollidable)command).OnCollisionExit;
                    ((IStateWithCollision)this).OnCollisionStayEvent += ((ICollidable)command).OnCollisionStay;
                }
                    
            }
        }

        /// <summary>
        /// passes the IAIuser to commands and transitions
        /// </summary>
        /// <param name="pOwner"></param>
        private void SetOwner(IAIUser pOwner)
        {
            owner = pOwner;
            foreach(ICommand command in commands)
            {
                command.SetOwner(pOwner);
            }
            foreach (ITransition transition in transitions)
            {
                transition.SetOwner(pOwner);
            }
        }
        /// <summary>
        /// Populates the commands array
        /// </summary>
        public abstract void SetCommands();
        /// <summary>
        /// Populates the transitions array
        /// </summary>
        public abstract void SetTransitions();
        /// <summary>
        /// Handles everything once the state has started
        /// </summary>
        public virtual void Begin() 
        {
            foreach (ICommand command in commands)
            {
                command.StartCommand();
            }
            foreach (ITransition transition in transitions)
            {
                transition.StartTransition();
            }
        }
        /// <summary>
        /// Handles everything once the state has ended
        /// </summary>
        public virtual void End() 
        {
            foreach (ICommand command in commands)
            {
                command.ExitCommand();
            }
            foreach (ITransition transition in transitions)
            {
                transition.ExitTransition();
            }
        }
        /// <summary>
        /// Executes the state behaviours
        /// </summary>
        /// <param name="deltaTime"></param>
        public virtual void Execute()
        {
            state = -1;
            
            //Execute commands
            foreach (ICommand command in commands)
            {
                command.Execute();
            }
            //Execute Transitions
            foreach (ITransition transition in transitions)
            {
                state = transition.ExecuteTransition();
                if (state != stateMachine.CurrentStateIndex)
                {
                    stateMachine.ChangeState(state);
                    break;
                }
            }
        }
        public virtual void LateUpdate()
        {
            
        }
        /// <summary>
        /// Return the current state index
        /// </summary>
        /// <returns></returns>
        public abstract int StateIndex();

        public virtual void FixedExecute()
        {
            
            //Execute commands
            foreach (ICommand command in commands)
            {
                command.FixedExecute();
            }
            //Execute Transitions
            foreach (ITransition transition in transitions)
            {
                transition.Condition.FixedUpdate();
            }
        }
    }
}
