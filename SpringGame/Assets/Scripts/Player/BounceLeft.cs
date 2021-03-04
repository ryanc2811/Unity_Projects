using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceLeft : Bounce
{
    protected override void HitCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 2f))
        {
            dir = hit.point - transform.position;
            dir.Normalize();
            applyForce = true;
            lastHitPoint = hit.point;
        }
    }
}
