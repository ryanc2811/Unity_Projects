using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClickHandler : MonoBehaviour
{
    bool one_click = false;
    bool timer_running;
    float timer_for_double_click;
    //this is how long in seconds to allow for a double click
    float delay = 0.2f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!one_click) // first click no previous clicks
            {
                one_click = true;
                timer_for_double_click = Time.time; // save the current time
                                                    // do one click things;
            }
            else
            {
                one_click = false; // found a double click, now reset

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Environment")))
                {
                    Vector3 position = hit.point;
                    position.y = 1f;
                    EventManager.instance.DoubleClickTrigger(position);
                }
            }
        }
        if (one_click)
        {
            // if the time now is delay seconds more than when the first click started. 
            if ((Time.time - timer_for_double_click) > delay)
            {
                //basically if thats true its been too long and we want to reset so the next click is simply a single click and not a double click.
                one_click = false;
            }
        }
    }
}
