using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellEffect : MonoBehaviour
{
    public float force=10f;
    protected Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public abstract void StartEffect(Vector3 direction);
}
