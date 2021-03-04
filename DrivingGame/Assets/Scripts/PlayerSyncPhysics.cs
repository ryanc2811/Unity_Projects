using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSyncPhysics : NetworkBehaviour
{
    [SerializeField]private Rigidbody rb;

    [Command]//function that runs on server when called by a client
    public void CmdResetPose()
    {
        rb.position = new Vector3(0, 1, 0);
        rb.velocity = Vector3.zero;
    }
    public void AddForce(Vector3 force, ForceMode FMode,Rigidbody rigidbody)//apply force on the client-side to reduce the appearance of lag and then apply it on the server-side
    {
        rigidbody.AddForce(force, FMode);
        if (base.isClientOnly)
            CmdAddForceToRigidbody(force, FMode,rigidbody.gameObject);
    }
    public void AddForce(Vector3 force, ForceMode FMode)//apply force on the client-side to reduce the appearance of lag and then apply it on the server-side
    {
        rb.AddForce(force, FMode);
        if (base.isClientOnly)
            CmdAddForce(force, FMode);
    }
    public void AddRelativeForce(Vector3 force, ForceMode FMode)//apply force on the client-side to reduce the appearance of lag and then apply it on the server-side
    {
        rb.AddRelativeForce(force, FMode);
        if (base.isClientOnly)
            CmdAddRelativeForce(force, FMode);
    }
    public void AddRelativeTorque(Vector3 force, ForceMode FMode)//apply force on the client-side to reduce the appearance of lag and then apply it on the server-side
    {
        rb.AddRelativeTorque(force, FMode);
        if (base.isClientOnly)
            CmdAddRelativeTorque(force, FMode);
    }

    [Command]//function that runs on server when called by a client
    public void CmdAddRelativeTorque(Vector3 force, ForceMode FMode)
    {
        rb.AddRelativeTorque(force, FMode);//apply the force on the server side
    }
    [Command]//function that runs on server when called by a client
    public void CmdAddForceToRigidbody(Vector3 force, ForceMode FMode,GameObject rigidbody)
    {
        rigidbody.GetComponent<Rigidbody>().AddForce(force, FMode);//apply the force on the server side
    }
    [Command]//function that runs on server when called by a client
    public void CmdAddForce(Vector3 force, ForceMode FMode)
    {
        rb.AddForce(force, FMode);//apply the force on the server side
    }
    [Command]//function that runs on server when called by a client
    public void CmdAddRelativeForce(Vector3 force, ForceMode FMode)
    {
        rb.AddRelativeForce(force, FMode);//apply the force on the server side
    }
}
