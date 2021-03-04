using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAudio : MonoBehaviour
{
    public AudioSource engineSound;
    private float enginePitch;
    [SerializeField] private const float lowPitch = .1f;
    [SerializeField] private const float highPitch = 2.0f;
    [SerializeField] private /*const*/ float speedToRevs = .05f;
    Vector3 myVelocity;
    Rigidbody carRigidBody;

    private void Awake()
    {
        carRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        myVelocity = carRigidBody.velocity;
        float forwardSpeed = transform.InverseTransformDirection(carRigidBody.velocity).z;
        float engineRevs = Mathf.Abs(forwardSpeed) * speedToRevs;
        engineSound.pitch = Mathf.Clamp(engineRevs, lowPitch, highPitch);
    }
}
