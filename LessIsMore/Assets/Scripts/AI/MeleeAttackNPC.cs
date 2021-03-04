using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MeleeAttackNPC : EnemyNPC
{
    #region Variables
    //DECLARE a Damage 
    public float dmg;
    //DECLARE a Attack Speed as and cooldown 
    public float attackSpeed;
    //DECLARE a Timer for attacks
    private float cooldownRate = 2f;
    //DECLARE a Player's Transform 
    private Transform Player;
    public EnemyType enemyType;

    public override EnemyType EnemyType { get { return enemyType; } }
    #endregion

    #region Start
    public void Start()
    {
        //ASSIGN Attack Speed to real time
        attackSpeed = Time.time;
        //Search for the nearest Player 
        GameObject p= GameObject.FindGameObjectWithTag("Player");
        //Assign founded player to a field you need
        Player = p.transform;
    }
    #endregion

    #region Update
    public void Update()
    {
        //DECLARE a distance to calculate beteewn player and enemy
        Vector2 distance = (transform.position-Player.position);
        //DECLARE Speed 
        float distanceFromPlayer = distance.magnitude;
        distance /= distanceFromPlayer;
        //If you can attack
        if (Time.time > attackSpeed + cooldownRate)
        {
            //And if you are close to the player
            if (distanceFromPlayer<1f)
            {
                //Reassign the timer
                attackSpeed = Time.time;
                //Deal damage to the player
                Player.GetComponent<ReceiveDamage>().damageTaken(dmg);
                //Print String "damage" to the console
                Debug.Log("damage");
            }
        }
    }
    #endregion
}
