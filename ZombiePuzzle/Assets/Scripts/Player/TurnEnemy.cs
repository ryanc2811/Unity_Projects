using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnemy : MonoBehaviour
{
    float rayLength = 2f;
    Vector3 posBeforeBite = Vector3.zero;
    public bool collidedWithEnemy { get; private set; }
    public bool ZombieSpawned { get; private set; }
    public GameObject ZombiePrefab;
    Vector3 enemyPos;
    // Start is called before the first frame update
    void Start()
    {
        posBeforeBite = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyBiteable())
        {
            
        }
        if (ZombieSpawned)
        {
            Invoke("ResetSpawner", 5f);
        }
    }
    void ResetSpawner()
    {
        ZombieSpawned = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy"&&!collidedWithEnemy)
        {
            collidedWithEnemy = true;
            enemyPos = other.transform.position;
            Destroy(other.gameObject,.2f);
            Invoke("CreateNewZombie", 1f);
        }
    }
    void CreateNewZombie()
    {
        Instantiate(ZombiePrefab, new Vector3(enemyPos.x,transform.position.y,enemyPos.z),Quaternion.identity);
        ZombieSpawned = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            collidedWithEnemy = false;
        }
    }

    bool EnemyBiteable()
    {

        Ray rayUp = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward);
        Ray rayLeft = new Ray(transform.position + new Vector3(0, 0.25f, 0), -transform.right);
        Ray rayRight = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.right);
        Ray rayDown = new Ray(transform.position + new Vector3(0, 0.25f, 0), -transform.forward);
        Ray[] rayArray = {rayDown, rayUp, rayLeft, rayRight};
        RaycastHit hit;

        foreach(Ray ray in rayArray)
        {
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    return true;
                }
            }
        }
       
        return false;
    }
}
