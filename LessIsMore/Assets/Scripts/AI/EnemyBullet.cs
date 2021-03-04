using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    #region Variables
    //DECLARE damage Dealt by the bullet
    public float damage;
    #endregion

    #region OnCollision
    //METHOD: that is called when bullet collides with the enemy
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if controller is player controller...
        if (collision.gameObject.tag != "Enemy")
        {
            //if There is Recieve damage class
            if (collision.GetComponent<ReceiveDamage>() != null)
            {
                //Deal damage to the game object
                collision.GetComponent<ReceiveDamage>().damageTaken(damage);
            }
            //Destroy bullet after
            Destroy(gameObject);

        }
    }
    #endregion
}
