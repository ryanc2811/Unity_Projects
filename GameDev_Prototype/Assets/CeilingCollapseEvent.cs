using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingCollapseEvent : MonoBehaviour
{
    public GameObject battleUI;

    bool battled = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&&!battled)
        {
            
            PlayerStats.Instance.StopMoving = true;
            battleUI.SetActive(true);
            battled = true;
        }
    }
}
