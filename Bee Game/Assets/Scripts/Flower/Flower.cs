using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public GameObject Pollen;

    /// <summary>
    /// When the Player colldes with a flower spawn some pollen
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Instantiate(Pollen,collider.transform.position, transform.rotation);
        }
    }
}
