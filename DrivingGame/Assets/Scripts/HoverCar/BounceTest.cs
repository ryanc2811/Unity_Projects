using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTest : MonoBehaviour
{
    private Vector3 velocityBeforeBounce = Vector3.zero;
    private Rigidbody rb;
    [SerializeField] private float bounciness = 30f;
    [SerializeField] private float pushForce = 100f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        velocityBeforeBounce = rb.velocity;
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("BounceSurface"))
        {
            Vector3 contactPoint = Vector3.zero;
            contactPoint = other.GetContact(0).point;
            Vector3 newDirection = (transform.position - new Vector3(contactPoint.x, transform.position.y, contactPoint.z)).normalized;
            Debug.Log(velocityBeforeBounce.magnitude);
            rb.AddForce(newDirection * velocityBeforeBounce.magnitude*bounciness,ForceMode.Force);
        }
        if (other.collider.CompareTag("Player"))
        {
            Rigidbody otherRb = other.collider.gameObject.GetComponent<Rigidbody>();
            Vector3 contactPoint = Vector3.zero;
            contactPoint = other.GetContact(0).point;
            Vector3 newDirection = (transform.position - new Vector3(contactPoint.x, otherRb.transform.position.y, contactPoint.z)).normalized;
            float speed = velocityBeforeBounce.magnitude;
            otherRb.AddForce(-newDirection * speed*pushForce, ForceMode.Force);
        }
    }
}
