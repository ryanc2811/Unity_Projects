using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphBehaviour : MonoBehaviour
{
    IMorphController morphController;
    // Start is called before the first frame update
    void Start()
    {
        morphController = GetComponent<IMorphController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MorphTrigger")
        {
            MorphType newIndex = other.GetComponent<IMorphTrigger>().TransitionState;
            
        }
    }
}
