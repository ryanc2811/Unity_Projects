using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;
    private Rigidbody2D rb;
    float fireSpeed = 5f;
    private Vector2 oldVelocity;
    float fadeSpeed = .3f;
    int bounceCount=0;
    [SerializeField] int maxBounces=3;

    //[SerializeField] AudioSource bounceSound;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.up * fireSpeed;
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        oldVelocity = rb.velocity;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            if (bounceCount < maxBounces)
            {
                ContactPoint2D contact = other.contacts[0];

                Vector2 reflectedVelocity = Vector2.Reflect(oldVelocity, contact.normal);

                rb.velocity = reflectedVelocity;
                //bounceSound.Play();
                Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
                transform.rotation = rotation * transform.rotation;
                bounceCount++;
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }
}
