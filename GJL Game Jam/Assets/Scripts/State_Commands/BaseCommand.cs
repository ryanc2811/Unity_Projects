using GameEngine.BehaviourManagement.StateMachine_Stuff;
using GameEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Commands
{
    public abstract class BaseCommand : ICommand
    {
        //DECLARE an IAIUser for storing a reference to the IAIUser that relate to this command
        protected IAIUser owner;
        /// <summary>
        /// SET the AIUser member
        /// </summary>
        /// <param name="pOwner"></param>
        public void SetOwner(IAIUser pOwner) => owner=pOwner;
        /// <summary>
        /// Executes the command
        /// </summary>
        public abstract void Execute();
        public virtual void FixedExecute() { }
        /// <summary>
        /// Starts the command
        /// </summary>
        public virtual void StartCommand() { }
        /// <summary>
        /// Stops the command
        /// </summary>
        public virtual void ExitCommand() { }
    }
}
