using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private IInteractable currentObject=null;
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
            currentObject = other.GetComponent<IInteractable>().Instance();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
            currentObject = null;
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Interact")&&currentObject!=null)
        //{
        //    if (currentObject is Lockable)
        //    {
        //        ((Lockable)currentObject).setInventory(PlayerInventory.Instance);
        //    }
        //    //INTERACTS WITH OBJECT
        //    currentObject.doInteraction();
        //}
    }
}
