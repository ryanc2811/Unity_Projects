using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : MonoBehaviour
{
   /// <summary>
   /// If the player collides with the pollen, then the player becomes the parent of the pollen
   /// </summary>
   /// <param name="collider"></param>
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            this.transform.parent = collider.gameObject.transform;
        }
            
    }
}
