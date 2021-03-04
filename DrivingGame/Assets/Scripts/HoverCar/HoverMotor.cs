using FirstGearGames.Mirrors.Assets.ReactivePhyics;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMotor : NetworkBehaviour
{
    public float speed = 40f;
    public float turnSpeed = 5f;
    public float hoverForce = 20f;
    public float hoverHeight = 3.5f;

    private float powerInput;
    private float turnInput;

    private PlayerSyncPhysics syncPhysics;
    /// <summary>
    /// Rigidbody on this object.
    /// </summary>
    //private Rigidbody rb;
    /// <summary>
    /// ReactivePhysicsObject on this object
    /// </summary>
    private ReactivePhysicsObject rpo;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        rpo = GetComponent<ReactivePhysicsObject>();
        syncPhysics=GetComponent<PlayerSyncPhysics>();
}

    public override void OnStartServer()
    {
        base.OnStartServer();
        syncPhysics = GetComponent<PlayerSyncPhysics>();
    }
    #region Client
    public void SetMovement(Vector2 movement)
    {
        if (base.hasAuthority)
        {
            powerInput = movement.y;
            turnInput = movement.x;
        }
            
    }
    void FixedUpdate()
    {
        if (base.hasAuthority)
            CheckMove();

    }
    void CheckMove()
    {
        MoveCar();
        rpo.ReduceAggressiveness();
    }

    void MoveCar()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            syncPhysics.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }

        syncPhysics.AddRelativeForce(new Vector3(0f, 0f, powerInput * speed),ForceMode.Force);
        syncPhysics.AddRelativeTorque(new Vector3(0f, turnInput * turnSpeed, 0f),ForceMode.Force);
    }
    #endregion
}
