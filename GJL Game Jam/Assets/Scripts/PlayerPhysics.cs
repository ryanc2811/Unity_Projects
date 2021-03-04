using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    public static PlayerPhysics Instance;

    float checkRadius = 0.2f;
    void Awake()
    {
        Instance = this;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(Globals.Instance.feetPos.position, checkRadius, Globals.Instance.whatIsGround);
    }
}
