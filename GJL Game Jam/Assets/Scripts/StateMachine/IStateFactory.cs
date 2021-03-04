using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.BehaviourManagement.StateMachine_Stuff
{
    public interface IStateFactory
    {
        /// <summary>
        /// Create a new IState
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>a new IState</returns>
        IState Create<T>() where T : IState, new();
    }
}
