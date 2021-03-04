using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Vector3 up = Vector3.zero,
        down = new Vector3(0, 180, 0),
        right = new Vector3(0, 90, 0),
        left = new Vector3(0, 270, 0),
        currentDir = Vector3.zero;

    Vector3 nextPos, destination, direction, lastPos;
    Vector2 currentGridPos;
    float speed = 5f;
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
        GameEvents.current.ZombieSpawnTrigger(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GridPoint"))
            currentGridPos = new Vector2(other.GetComponent<GridStat>().x, other.GetComponent<GridStat>().y);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        Move();
    }
    public void Move(/*string Direction*/)
    {
        //if (Direction=="Up")
        //{
        //    nextPos = Vector3.forward * 2;
        //    lastPos = nextPos;
        //    currentDir = up;
        //    canMove = true;
        //}
        //else if (Direction == "Down")
        //{
        //    nextPos = Vector3.back * 2;
        //    lastPos = nextPos;
        //    currentDir = down;
        //    canMove = true;

        //}
        //else if (Direction == "Left")
        //{
        //    nextPos = Vector3.left * 2;
        //    lastPos = nextPos;
        //    currentDir = left;
        //    canMove = true;
        //}
        //else if (Direction == "Right")
        //{
        //    nextPos = Vector3.right * 2;
        //    lastPos = nextPos;
        //    currentDir = right;
        //    canMove = true;
        //}
        if (Input.GetKey(KeyCode.W))
        {
            GameObject nextNode=GridMap.Instance.GetPath(currentGridPos, 1)[0];
            destination = nextNode.transform.position;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            GameObject nextNode = GridMap.Instance.GetPath(currentGridPos, 2)[0];
            destination = nextNode.transform.position;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            GameObject nextNode = GridMap.Instance.GetPath(currentGridPos, 3)[0];
            destination = nextNode.transform.position;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            GameObject nextNode = GridMap.Instance.GetPath(currentGridPos, 4)[0];
            destination = nextNode.transform.position;
        }
        

        //if (Vector3.Distance(destination, transform.position) <= 0.0001f)
        //{
        //    transform.localEulerAngles = currentDir;
        //    if (canMove)
        //    {
        //        if (Valid())
        //        {
        //            destination = transform.position + nextPos;
        //            direction = nextPos;
        //            canMove = false;
        //            GameEvents.current.PlayerMoveTrigger(id);
        //        }
        //    }
        //}
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

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Player"))
            {
                return false;
            }
        }
        return true;

    }
}
