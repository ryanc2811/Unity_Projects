using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{

    Vector2 movement { get; set; }
    Vector2 direction { get; set; }
    Rigidbody2D RB { get; set; }
    Transform TF { get; set; }
    float Speed { get; set; }
        
    
    void RequestChangeState(IState requestedState);
    
    
}
