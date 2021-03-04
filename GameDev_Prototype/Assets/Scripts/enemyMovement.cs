using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector3 targetPosition;
    private float speed = 5f;

    public GameObject heartPrefab;
    private GameObject currentHeart;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = PositionInArea(new Vector3(0, 0, 0), new Vector3(10f, 10f, 0));
    }


    void Roam()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 1f)
        {
            Vector3 direction=(targetPosition-transform.position).normalized;
            rb.velocity = new Vector3(direction.x*speed,direction.y*speed,0);
        }
        else
        {
            targetPosition= PositionInArea(new Vector3(0, 0, 0), new Vector3(10f, 10f, 0));
        }
    }
    Vector3 PositionInArea(Vector3 center, Vector3 size)
    {
        Vector3 position= center + new Vector3((Random.value - 0.5f) * size.x, (Random.value - 0.5f) * size.y, 0f);
        if(currentHeart)Destroy(currentHeart);
        currentHeart=Instantiate(heartPrefab, position, Quaternion.identity);
        return position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Roam();
    }
}
