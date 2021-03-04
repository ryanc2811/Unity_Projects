using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveDirection;
    Rigidbody rb;
    float speed = 5f;
    float maxSpeed = 10f;
    Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));

        RaycastHit[] hitpoints = Physics.RaycastAll(ray).OrderBy(h => h.distance).ToArray(); //sort by distance
        foreach (RaycastHit hit in hitpoints)
        {
            if (hit.collider.gameObject != this.gameObject)
            {
                Vector2 newPos = hit.point;
                Vector2 direction = new Vector2(transform.position.x, transform.position.y) - newPos;
                rb.AddForce(direction * -speed);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
                break; //stop iterating
            }
        }
    }
}
