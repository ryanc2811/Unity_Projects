using GameEngine.BehaviourManagement.StateMachine_Stuff;
using GameEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.State_Conditions
{
    public abstract class BaseCondition : ICondition
    {
        //DECLARE an IAIUser for storing a reference to the ai user that relates to the current state
        protected IAIUser owner;
        /// <summary>
        /// Returns the outcome of the condition
        /// </summary>
        /// <returns></returns>
        public abstract bool FindOutcome();
        /// <summary>
        /// Close the condition
        /// </summary>
        public virtual void ExitCondition() { }
        /// <summary>
        /// Start the condition
        /// </summary>
        public virtual void StartCondition() { }
        /// <summary>
        /// Sets the IAIUser member
        /// </summary>
        /// <param name="pOwner"></param>
        public void SetOwner(IAIUser pOwner)
        {
            owner = pOwner;
        }

        public virtual void FixedUpdate() { }
    }
}
