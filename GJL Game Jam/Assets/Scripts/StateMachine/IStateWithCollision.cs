using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.BehaviourManagement.StateMachine_Stuff
{
    public interface IStateWithCollision
    {
        void OnCollisionEnter(Collision2D other);
        void OnCollisionStay(Collision2D other);
        void OnCollisionExit(Collision2D other);
        void OnTriggerEnter(Collider2D other);
        void OnTriggerExit(Collider2D other);
        Action<Collision2D> OnCollisionEnterEvent { get; set; }
        Action<Collision2D> OnCollisionExitEvent { get; set; }
        Action<Collision2D> OnCollisionStayEvent { get; set; }
        Action<Collider2D> OnTriggerEnterEvent { get; set; }
        Action<Collider2D> OnTriggerExitEvent { get; set; }
    }
}
