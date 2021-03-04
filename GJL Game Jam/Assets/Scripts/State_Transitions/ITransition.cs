using GameEngine.BehaviourManagement.StateMachine_Stuff;
using GameEngine.State_Conditions;
using GameEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Transitions
{
    public interface ITransition
    {
        /// <summary>
        /// Execute transition
        /// </summary>
        int ExecuteTransition();

        /// <summary>
        /// Gets the reference to the condition object
        /// </summary>
        ICondition Condition { get; }
        string Name{ get; }
        /// <summary>
        /// Starts the transition
        /// </summary>
        void StartTransition();
        /// <summary>
        /// Closes the transition
        /// </summary>
        void ExitTransition();
        /// <summary>
        /// passes the AIUser to condition
        /// </summary>
        /// <param name="owner"></param>
        void SetOwner(IAIUser owner);
    }
}
