using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RangeAttackNPC : EnemyNPC
{
    //DECLARE A point where to insiate the bullets
    private Transform aimTransform;
    //DECLARE A Transform for getting the position at which the npc shoot shoot from
    public Transform shootPoint;
    //DECLARE A Transform for getting the position of the Enemies' Target
    public Transform targetPos;
    //DECLARE A GameObject for storing a reference to the targets gameobject
    private GameObject TargetObj;
    //DECLARE A Vector2 for the distance between this and the target
    private Vector2 distance;
    //DECLARE A float for the distance between this and the target
    private float distanceFrom;
    //DECLARE A float for the time between each shot
    public float fireAtWhen;
    //DECLARE A float for the damage of the bullet
    public float damage;
    //DECLARE A GameObject for  the projectile
    public GameObject projectile;
    //DECLARE A float for the force of the projectile
    public float force;
    //DECLARE A float for the fire rate of the projectile
    public float fireRate;
    //DECLARE A float for the time of the last shot fired
    private float lastShot;
    public EnemyType enemyType;
    public override EnemyType EnemyType { get { return enemyType; } }
    void Start()
    {
        //store the trasform of the gameobject with the tag 'Aim'
        aimTransform = transform.Find("Aim");
        //store the gameobject of the player
        TargetObj = GameObject.FindGameObjectWithTag("Player");
        //store the transform player
        targetPos = TargetObj.transform;
    }
    // Update is called once per frame
    void Update()
    {
        //Calculate distance from player
        distance = (transform.position - targetPos.position);
        distanceFrom = distance.magnitude;
        distance /= distanceFrom;
        //if in range, shoot
        if (distanceFrom < fireAtWhen)
        {
            shoot();
        }


    }
    /// <summary>
    /// Shoots projectiles
    /// </summary>
    public void shoot()
    {
        //if the enemy can shoot
        if (Time.time > fireRate + lastShot)
        {
            //instantiate a bullet projectile
            GameObject bullet = Instantiate(projectile, shootPoint.position, Quaternion.identity);
            
            shootPoint.LookAt(targetPos);
            aimTransform.LookAt(targetPos);
            Vector3 aimDirection = (targetPos.position - transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            //aimTransform.eulerAngles = new Vector3(0, 0, angle);
            bullet.GetComponent<Rigidbody2D>().velocity = aimDirection * force;
            bullet.GetComponent<EnemyBullet>().damage = damage;
            lastShot = Time.time;
        }
    }
}
