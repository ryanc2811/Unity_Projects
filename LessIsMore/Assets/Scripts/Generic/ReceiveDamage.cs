using Assets.Scripts.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReceiveDamage : MonoBehaviour
{
    //DECLARE A FLOAT FOR STORING THE MAX HEALTH OF THE ENTITY
    public float maxHealth;
    //DECLARE A FLOAT FOR THE CURRENT HEALTH OF THE ENTITY
    public float health;
    //DECLARE A HEALTH BAR FOR THE ENTITY
    public HealthBar healthBar;
    //DECLARE A CHARACTER ATTRIBUTES FOR THE ENTTIY
    private CharacterAttributes attributes;
    //DECLARE A BOOLEAN FOR CHECKING IF THE ENTITY IS DEAD
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        //SET HEALTH TO MAX HEALTH
        health = maxHealth;
        //SET THE HEALTH BAR VALUE TO THE MAX HEALTH OF THE ENTITY
        healthBar.SetMaxHealth(maxHealth);
        //SET THE HEALTH BAR TO INACTIVE BY DEFAULT
        healthBar.slider.transform.gameObject.SetActive(false);
        //assign the give damage commponent
        attributes = gameObject.GetComponent<CharacterAttributes>();
    }
    /// <summary>
    /// TRIGGERS WHEN THE ENTITY HAS TAKEN DAMAGE
    /// </summary>
    /// <param name="damage"></param>
    public void damageTaken(float damage)
    {
        damage -= attributes.strength.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        //TAKE DAMAGE
        health -= damage;
        //SET THE HEALTH BAR ACTIVE
        healthBar.slider.transform.gameObject.SetActive(true);
        //CHECK IF THE ENTITY IS DEAD
        checkDeath();
    }
    /// <summary>
    /// CHECKS TO SEE IF THE ENTITY IS DEAD
    /// </summary>
    void checkDeath()
    {
        //IF ENTITY IS DEAD
        if (health <= 0)
        {
            //SET THE HEALTH BAR TO THE CURRENT HEALTH
            healthBar.SetHealth(health);
            //IF THE PLAYER IS DEAD
            if (gameObject.CompareTag("Player"))
                //RELOAD CURRENT SCENE
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else
            {
                //DESTROY THE GAMEOBJECT
                Destroy(gameObject);
                
                        }
        }
       
    }
    void Update()
    {
        
        healthBar.SetHealth(health);
    }
}
