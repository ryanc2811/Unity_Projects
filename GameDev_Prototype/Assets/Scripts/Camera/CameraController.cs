using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]private float trackSpeed = 10;


    // Set target
    public void SetTarget(Transform t)
    {
        target = t;
    }

    // Track target
    void FixedUpdate()
    {
        if (target)
        {
            var v = transform.position;
            v.x = target.position.x;
            v.y = target.position.y;
            transform.position = Vector3.MoveTowards(transform.position, v, trackSpeed * Time.deltaTime);
        }
    }
}
