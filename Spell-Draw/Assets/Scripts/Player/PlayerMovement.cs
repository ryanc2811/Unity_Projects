using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    GameObject relic;
    float distanceFromRelic;
    float minDistance = 15f;
    float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        relic = GameObject.FindGameObjectWithTag("Relic");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceFromRelic = Vector3.Distance(relic.transform.position,transform.position);
        if (distanceFromRelic > minDistance)
        {
            Vector3 direction = (relic.transform.position-transform.position).normalized;
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }
}
