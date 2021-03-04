using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    //Declare time after which particle should be destroyed
    public float DestroyAfter;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy gparticles after given amount of time
        Destroy(gameObject, DestroyAfter);
        
    }

 
}
