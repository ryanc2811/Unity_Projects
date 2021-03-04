using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Entities;
using GameEngine.BehaviourManagement;
using UnityEngine;
namespace GameEngine.Collision
{
    public interface ICollidable
    {
        //Passes Entity that has been collided with
        void OnCollisionEnter(Collision2D other);
        void OnCollisionStay(Collision2D other);
        void OnCollisionExit(Collision2D other);
        void OnTriggerEnter(Collider2D other);
        void OnTriggerExit(Collider2D other);
    }
}
