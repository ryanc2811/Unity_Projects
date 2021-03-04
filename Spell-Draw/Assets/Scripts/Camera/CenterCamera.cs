using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCamera : MonoBehaviour
{
    public GameObject Target;
    public static CenterCamera instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LockCameraToTarget();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LockCameraToTarget();
        }
    }
    public void LockCameraToTarget()
    {
        transform.LookAt(Target.transform.position);
    }
}
