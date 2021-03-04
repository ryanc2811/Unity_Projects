using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class CameraFollowAim : MonoBehaviour
{

    private GameObject player;
    private GameObject cameraHolder;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
        cameraHolder = GameObject.FindGameObjectWithTag("CameraParent");
    }
    
    void FixedUpdate()
    {
        //FOLLOW THE MOUSE POSITION
        float speed = 3f;
        transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, 0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(-3, -100f, 100f));
    }
}
