using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardAcc = 8f, reverseAcc = 4f, maxSpeed = 50f, turnStrength = 180f,gravityForce=10f,dragOnGround=3f;
    private float speedInput, turnInput;
    private bool isGrounded = false;

    public LayerMask whatIsGround;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;

    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTurn=25f;

    public ParticleSystem[] dustTrail;
    public float maxEmission = 25f;
    private float emissionRate;
    // Start is called before the first frame update
    void Start()
    {
        rb.transform.parent = null;
    }

    public void Move(Vector3 movement)
    {
        speedInput = movement.x;
        turnInput = movement.z;
        if (isGrounded)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * movement.z, 0f));
        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x,
            turnInput * maxWheelTurn, rightFrontWheel.localRotation.eulerAngles.z);
        transform.position = rb.transform.position;
    }
    //// Update is called once per frame
    //void Update()
    //{
    //    if (this.isLocalPlayer)
    //    {
    //        speedInput = 0;
    //        if (Input.GetAxis("Vertical") > 0)
    //        {
    //            speedInput = Input.GetAxis("Vertical") * forwardAcc * 1000f;
    //        }
    //        else if (Input.GetAxis("Vertical") < 0)
    //            speedInput = Input.GetAxis("Vertical") * reverseAcc * 1000f;

    //        turnInput = Input.GetAxis("Horizontal");
    //        if (isGrounded)
    //            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
    //        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z);
    //        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, rightFrontWheel.localRotation.eulerAngles.z);

    //        transform.position = rb.transform.position;
    //    }

    //}
    void FixedUpdate()
    {
        isGrounded = false;
        RaycastHit hit;
        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            isGrounded = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        emissionRate = 0;
        if (isGrounded)
        {
            rb.drag = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                rb.AddForce(transform.forward * (speedInput*1000f));
                emissionRate = maxEmission;
            }
        }
        else
        {
            rb.drag = 0.1f;
            rb.AddForce(Vector3.up * -gravityForce * 100f);
        }
        foreach (ParticleSystem part in dustTrail)
        {
            var emissionModule = part.emission;
            emissionModule.rateOverTime = emissionRate;
        }
    }
}
