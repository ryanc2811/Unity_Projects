using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameEngine.Entities
{
    public interface IAIUser
    {
        //Property for AIUsers Transform
        Transform Transform { get;}
        Rigidbody2D RB { get; }
        //Setter for Position
        void SetPosition(float x, float y);
        void SetVelocity(float x, float y);
        Animator Anim { get; }
        Transform Target { get; }
    }
}
