using GameEngine.Commands;
using GameEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.BehaviourManagement.StateMachine_Stuff
{
    public interface IState
    {
        /// <summary>
        /// Starts the states transition
        /// </summary>
        void Begin();
        /// <summary>
        /// Exectutes the states behaviour
        /// </summary>
        void Execute();
        void LateUpdate();
        void FixedExecute();
        /// <summary>
        /// Ends the State transition
        /// </summary>
        void End();
        /// <summary>
        /// Returns the states index
        /// </summary>
        /// <returns></returns>
        int StateIndex();
        /// <summary>
        /// Initialises the state
        /// </summary>
        /// <param name="stateMachine"></param>
        /// <param name="owner"></param>
        void InitState(IStateMachine stateMachine, IAIUser owner);
    }
}
