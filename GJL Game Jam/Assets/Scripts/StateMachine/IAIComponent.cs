using GameEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.BehaviourManagement
{
    public interface IAIComponent
    {
        /// <summary>
        /// Gets the AI User of the AIComponent
        /// </summary>
        /// <param name="aiUser"></param>
        IAIUser GetAIUser();
        /// <summary>
        /// Sets the AI User of the AIComponent
        /// </summary>
        /// <param name="aiUser"></param>
        void SetAIUser(IAIUser aiUser);
        
        /// <summary>
        /// Runs after Content is loaded
        /// </summary>
        void OnContentLoad();
        /// <summary>
        /// Initialises the AIComponent
        /// </summary>
        void Initialise();
    }
}
