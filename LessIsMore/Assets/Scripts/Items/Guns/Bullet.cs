using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage=0;
    public GameObject particles;

    /// <summary>
    /// COLLISION FOR THE BULLET
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if controller is player controller...
        if (collision.gameObject.tag!="Player")
        {
            //DAMAGE THE PLAYER
            if (collision.GetComponent<ReceiveDamage> ()!= null)
            {
                collision.GetComponent<ReceiveDamage>().damageTaken(damage);
            }
            Instantiate(particles, transform.position, transform.rotation);
                Destroy(gameObject);
            
        }
    }



   
}
