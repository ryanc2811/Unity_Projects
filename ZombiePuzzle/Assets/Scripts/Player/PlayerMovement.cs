using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    Vector3 up = Vector3.zero,
        down = new Vector3(0, 180, 0),
        right = new Vector3(0, 90, 0),
        left = new Vector3(0, 270, 0),
        currentDir=Vector3.zero;

    Vector3 nextPos, destination, direction,lastPos;
    float speed=5f;
    bool canMove = false;
    float rayLength = 2f;
    bool returning = false;
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        id = gameObject.GetInstanceID();
        currentDir = up;
        nextPos = Vector3.forward;
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy"&&!returning)
        {
            returning = true;
            Invoke("ReturnToPosition", .5f);
        }
    }
    void ReturnToPosition()
    {
        if (Valid())
        {
            destination = transform.position - lastPos;
            direction = lastPos;
            canMove = false;
            returning = false;
            Debug.Log("Returning");
        }
        else
        {
            CheckNearestOpenSpace();
        }
    }
    void Move()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        #region input
        if (!returning&&!GetComponent<TurnEnemy>().ZombieSpawned)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                nextPos = Vector3.forward * 2;
                lastPos = nextPos;
                currentDir = up;
                canMove = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                nextPos = Vector3.back * 2;
                lastPos = nextPos;
                currentDir = down;
                canMove = true;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                nextPos = Vector3.left * 2;
                lastPos = nextPos;
                currentDir = left;
                canMove = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                nextPos = Vector3.right * 2;
                lastPos = nextPos;
                currentDir = right;
                canMove = true;
            }
        }
        
        #endregion
        if (Vector3.Distance(destination, transform.position) <= 0.0001f)
        {
            transform.localEulerAngles = currentDir;
            if (canMove)
            {
                if (Valid())
                {
                    destination = transform.position + nextPos;
                    direction = nextPos;
                    canMove = false;
                    GameEvents.current.PlayerMoveTrigger(id);
                }
            }  
        }
    }
    void CheckNearestOpenSpace()
    {
        Ray rayUp = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward);
        Ray rayLeft = new Ray(transform.position + new Vector3(0, 0.25f, 0), -transform.right);
        Ray rayRight = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.right);
        Ray rayDown = new Ray(transform.position + new Vector3(0, 0.25f, 0), -transform.forward);
        Ray[] rayArray = { rayDown, rayUp, rayLeft, rayRight };
        RaycastHit hit;
        bool foundSpace = false;
        foreach (Ray ray in rayArray)
        {
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                if (!hit.collider.CompareTag("Wall")&& !foundSpace || !hit.collider.CompareTag("Player")&&!foundSpace)
                {
                    foundSpace = true;
                    if (ray.direction == rayUp.direction)
                    {
                        nextPos = Vector3.forward * 2;
                        lastPos = nextPos;
                        currentDir = up;
                    }
                    else if (ray.direction == rayDown.direction)
                    {
                        nextPos = Vector3.back * 2;
                        lastPos = nextPos;
                        currentDir = down;
                    }
                    else if (ray.direction == rayLeft.direction)
                    {
                        nextPos = Vector3.left * 2;
                        lastPos = nextPos;
                        currentDir = left;
                    }
                    else if (ray.direction == rayRight.direction)
                    {
                        nextPos = Vector3.right * 2;
                        lastPos = nextPos;
                        currentDir = right;
                    }
                    destination = transform.position + nextPos;
                    direction = nextPos;
                    canMove = false;
                    returning = false;
                }
            }
        }

    }
    bool Valid()
    {
        Ray ray;
        if (returning)
            ray = new Ray(transform.position + new Vector3(0, 0.25f, 0), -transform.forward);
        else
            ray = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if(Physics.Raycast(ray,out hit, rayLength))
        {
            if (hit.collider.CompareTag("Wall")||hit.collider.CompareTag("Player"))
            {
                return false;
            }
        }
        return true;

    }
}
