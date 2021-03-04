using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBackTrap : MonoBehaviour
{
    float force = 50f;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 dir = other.contacts[0].point - other.transform.position;
            dir = -dir.normalized;
            other.gameObject.GetComponent<Rigidbody>().AddForce(dir * force,ForceMode.Impulse);
        }
    }
}
