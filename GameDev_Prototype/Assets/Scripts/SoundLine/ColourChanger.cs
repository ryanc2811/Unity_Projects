using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChanger : MonoBehaviour
{
    [SerializeField] private Material materialToAdd;
    [SerializeField] private Material defaultMaterial;
    public bool stopColourChange = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!stopColourChange)
        {
            //if a sound line hits the collider
            if (other.CompareTag("SoundLine"))
            {
                TrailRenderer line = other.GetComponentInChildren<TrailRenderer>();
                line.material = materialToAdd;
            }
        }
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (!stopColourChange)
        {
            //if a sound line hits the collider
            if (other.CompareTag("SoundLine"))
            {
                TrailRenderer line = other.GetComponentInChildren<TrailRenderer>();
                line.material = defaultMaterial;
            }
        }
    }
}
