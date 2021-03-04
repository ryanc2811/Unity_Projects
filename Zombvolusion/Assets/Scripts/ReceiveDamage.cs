using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDamage : MonoBehaviour
{
    public int health { get; private set; }
    public bool IsDead { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Attributes>().maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if(health>0)
            health -= damage;

        if (health <= 0)
        {
            IsDead = true;
            Destroy(gameObject);
            GameObject pcGO = GameObject.FindGameObjectWithTag("PlayerController");
            if (pcGO)
            {
                PlayerController playerController = pcGO.GetComponent<PlayerController>();
                if (playerController.Mover == gameObject && playerController.Mover != null)
                    EventManager.instance.PlayerDeathTrigger(gameObject);
                else
                {
                    EventManager.instance.AIDeathTrigger(gameObject);
                }
            }
        }
    }
}
