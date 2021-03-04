using GameEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Commands
{
    public interface ICommand
    {
        void FixedExecute();
        /// <summary>
        /// Execute command
        /// </summary>
        void Execute();
        /// <summary>
        /// Starts the command
        /// </summary>
        void StartCommand();
        /// <summary>
        /// Closes the command
        /// </summary>
        void ExitCommand();
        /// <summary>
        /// Sets the ai user member
        /// </summary>
        /// <param name="owner"></param>
        void SetOwner(IAIUser owner);
    }
}
