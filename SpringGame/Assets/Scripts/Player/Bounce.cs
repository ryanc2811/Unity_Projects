using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bounce : MonoBehaviour
{
    
    protected float force=10f;
    protected float boostedForce = 50f;
    protected float defaultForce;
    protected bool applyForce = false;
    protected Vector3 dir = Vector3.zero;
    protected GameObject player;
    public AudioClip bounceSound;
    protected Rigidbody rb;
    protected bool trigger = false;
    protected Vector3 lastHitPoint;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        defaultForce = force;
    }
    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject != player)
        {
                applyForce = false;
                rb.AddForce(-dir * force, ForceMode.Impulse);
                SoundManager.instance.PlaySound(bounceSound);
                trigger = true;
        }
        
    }
    void OnTriggerExit(Collider collider)
    {
        trigger = false;
    }
    protected abstract void HitCheck();
    void FixedUpdate()
    {

        HitCheck();
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, lastHitPoint);
        Debug.Log(dist);
        if (dist > 10f)
        {
            
        }
        if (player.GetComponent<PlayerMovement>().Boosted)
            force = boostedForce;
        else
            force = defaultForce;
    }
}
