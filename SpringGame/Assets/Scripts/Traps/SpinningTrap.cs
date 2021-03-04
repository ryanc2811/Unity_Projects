using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningTrap : PushBackTrap
{
    Transform topPoint;
    Transform bottomPoint;
    bool movingDown = false;
    [SerializeField]
    [Range(5f, 20f)]
    private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        topPoint = gameObject.transform.Find("TopPoint");
        bottomPoint = gameObject.transform.Find("BottomPoint");
        topPoint.SetParent(null, true);
        bottomPoint.SetParent(null, true);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        if (!movingDown)
        {
            transform.position=Vector3.MoveTowards(transform.position, topPoint.position, step);
            if (Vector3.Distance(transform.position, topPoint.position) < 0.0001f)
            {
                movingDown = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, bottomPoint.position, step);
            if (Vector3.Distance(transform.position, bottomPoint.position) < 0.0001f)
            {
                movingDown = false;
            }
        }

    }
}
