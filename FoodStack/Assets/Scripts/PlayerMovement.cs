using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float movement;
    Rigidbody rb;
    float speed = 5f;
    float startX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, startX - 5f, startX + 5f),transform.position.y,transform.position.z);
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * speed, 0f);
    }
}
