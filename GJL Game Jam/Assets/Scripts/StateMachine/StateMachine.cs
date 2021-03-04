using GameEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameEngine.BehaviourManagement.StateMachine_Stuff
{
    /// <summary>
    /// The mind of the AI component
    /// </summary>
    class StateMachine: IStateMachine, IAIComponent
    {
        //DECLARE an IDictionary for storing references to each state within the state machine
        private IDictionary<int, IState> allStates = new Dictionary<int, IState>();

        //DECLARE an IState for storing a reference to the current state
        protected IState currentState;
        //DECLARE an IAIUser for storing a reference to the AIUser that relates to the the statemachine
        protected IAIUser gameObject;
        //DECLARE an Action for storing an event that is triggered when the state changes
        protected event Action<int> OnStateChanged = delegate { };
        //DECLARE an integer for storing the index of the current state
        protected int currentStateIndex;
        //DECLARE an IDictionary for returning the states dictionary
        public IDictionary<int, IState> AllStates => allStates;
        //DECLARE an int for returning the current state index
        public int CurrentStateIndex => currentStateIndex;

        /// <summary>
        /// Initialises the statemachine
        /// </summary>
        /// <param name="pState"></param>
        public void InitialiseStateMachine(IState pState= null)
        {
            //Set the current state index to the index of the first value in the dictionary
            currentStateIndex = AllStates.First().Value.StateIndex();
            //Set the current state to the first value in the dictionary
            currentState = pState ?? AllStates.Values.First();
            
            foreach(KeyValuePair<int,IState> entry in AllStates)
            {
                //INITIALISE STATE
                entry.Value.InitState(this, gameObject);
            }

            //Start the current state
            currentState.Begin();
        }

        /// <summary>
        /// Change the current state to the new passed state 
        /// </summary>
        /// <param name="pStateEnum"></param>
        /// <param name="command"></param>
        public void ChangeState(int newStateIndex)
        {
            //if the state exists within the dictionary
            if (allStates[newStateIndex] != null)
            {
                //if there is already a current state
                if (currentState != null)
                {
                    //end the state
                    currentState.End();
                }
                currentStateIndex = newStateIndex;
                IState newState = allStates[newStateIndex];
                //set the current state to the new passed state
                currentState = newState;
                //Start the new state
                currentState.Begin();
                //Invoke State Changed Event
                OnStateChanged(newStateIndex);
            }
        }
        
        /// <summary>
        /// Removes a state from the dictionary
        /// </summary>
        /// <param name="pStateEnum"></param>
        public void RemoveState(int stateIndex)
        {
            if (allStates[stateIndex]!=null)
            {
                allStates.Remove(stateIndex);
            }
        }
        public void LateUpdate()
        {
            if (currentState != null)
                currentState.LateUpdate();
        }
        /// <summary>
        /// Update the state
        /// </summary>
        public void Update()
        {
            if (currentState != null)
                currentState.Execute();

            //Debug.Log((BaseEnemyStates)currentStateIndex);
        }
        public void FixedUpdate()
        {
            if (currentState != null)
                currentState.FixedExecute();
        }
       /// <summary>
       /// Sets the local AIUser
       /// </summary>
       /// <param name="aiUser"></param>
        #region AIComponent
        public void SetAIUser(IAIUser aiUser)
        {
            gameObject = aiUser;
        }
        /// <summary>
        /// Initialise StateMachine
        /// </summary>
        public virtual void Initialise()
        {
            InitialiseStateMachine();
        }
        /// <summary>
        /// Initialises states once content is loaded
        /// </summary>
        public void OnContentLoad()
        {
            
        }
        /// <summary>
        /// Returns the IAIUser related to the statemachine
        /// </summary>
        /// <returns></returns>
        public IAIUser GetAIUser()
        {
            return gameObject;
        }
        /// <summary>
        /// Adds a state to the dictionary
        /// </summary>
        /// <param name="pStateIndex"></param>
        /// <param name="pState"></param>
        public void AddState(int pStateIndex, IState pState) 
        {
            allStates.Add(pStateIndex, pState);
        }
        #endregion
    }
}
