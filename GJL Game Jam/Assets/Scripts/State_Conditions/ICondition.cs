using GameEngine.BehaviourManagement.StateMachine_Stuff;
using GameEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.State_Conditions
{
    public interface ICondition
    {
        /// <summary>
        /// Returns the outcome of the condition
        /// </summary>
        bool FindOutcome();
        void FixedUpdate();
        /// <summary>
        /// Starts the condition
        /// </summary>
        void StartCondition();
        /// <summary>
        /// Closes the condition
        /// </summary>
        void ExitCondition();
        /// <summary>
        /// Sets the IAIuser member
        /// </summary>
        /// <param name="owner"></param>
        void SetOwner(IAIUser owner);
    }
}
