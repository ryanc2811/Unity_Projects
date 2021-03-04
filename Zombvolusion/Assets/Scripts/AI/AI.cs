using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class AI : MonoBehaviour
{
    protected GameObject target;
    protected Attributes attributes;
    
    void Start()
    {
        attributes = GetComponentInParent<Attributes>();
    }
    
}
