using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewinder : MonoBehaviour
{
    bool canRewind = false;
    IRewindable rewindObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject target = hit.transform.gameObject;
            
            if (target.CompareTag("Rewindable"))
            {
                canRewind = true;
                rewindObj = target.GetComponent<Rewindable>();
            }
            else
            {
                canRewind = false;
            }
        }
        if (canRewind && Input.GetMouseButton(0))
        {
            rewindObj.Rewind();
        }
    }
}
