using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : NetworkBehaviour
{
    private Vector3 velocityBeforeBounce = Vector3.zero;
    private Rigidbody rb;
    private PlayerSyncPhysics syncPhysics;
    [SerializeField] private float bounciness = 30f;
    [SerializeField] private float pushForce = 100f;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        syncPhysics = GetComponent<PlayerSyncPhysics>();
        rb = GetComponent<Rigidbody>();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
    }
    void FixedUpdate()
    {
        if (base.hasAuthority)
            velocityBeforeBounce = rb.velocity;
    }
    void OnCollisionEnter(Collision other)
    {
        if (base.hasAuthority)
        {
            if (other.collider.CompareTag("BounceSurface"))
            {
                Vector3 contactPoint = Vector3.zero;
                contactPoint = other.GetContact(0).point;
                Vector3 newDirection = (transform.position - new Vector3(contactPoint.x, transform.position.y, contactPoint.z)).normalized;
                Debug.Log(velocityBeforeBounce.magnitude);
                syncPhysics.AddForce(newDirection * velocityBeforeBounce.magnitude * bounciness, ForceMode.Force);
            }
            if (other.collider.CompareTag("Player"))
            {
                Rigidbody otherRb = other.collider.gameObject.GetComponent<Rigidbody>();
                Vector3 contactPoint = Vector3.zero;
                contactPoint = other.GetContact(0).point;
                Vector3 newDirection = (transform.position - new Vector3(contactPoint.x, otherRb.transform.position.y, contactPoint.z)).normalized;
                float speed = velocityBeforeBounce.magnitude;
                syncPhysics.CmdAddForceToRigidbody(-newDirection * speed * pushForce, ForceMode.Force,otherRb.gameObject);
            }
        }
    }
}
