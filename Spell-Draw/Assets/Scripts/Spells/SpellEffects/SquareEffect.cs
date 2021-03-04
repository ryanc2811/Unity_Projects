using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEffect : SpellEffect
{
    Vector3 direction;
    bool trigger = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void StartEffect(Vector3 direction)
    {

        this.direction = direction;
        trigger = true;
        Debug.Log("Vroom");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(trigger)
            rb.AddForce(-direction * force);
    }
}
