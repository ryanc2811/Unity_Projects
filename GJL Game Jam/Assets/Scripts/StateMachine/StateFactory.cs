using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.BehaviourManagement.StateMachine_Stuff
{
    class StateFactory : IStateFactory
    {
        /// <summary>
        /// Create an IState
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>a new IState</returns>
        public IState Create<T>() where T : IState, new()
        {
            IState state = new T();
            return state;
        }
    }
}
