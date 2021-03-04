using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorController : MonoBehaviour
{
    [SerializeField] private int doorIndex;

    [SerializeField] private AudioSource openSound;

    bool opened = false;
    void Start()
    {
        EventManager.instance.OnDoorOpenTrigger += OnDoorOpenTrigger;
    }

    private void OnDoorOpenTrigger(int index)
    {
        if (index == doorIndex&&!opened)
            Open();
    }
    void Open()
    {
        openSound.Play();
        GetComponent<Collider2D>().enabled = false;
        EventManager.instance.ScanRequiredTrigger();
        opened = true;
        Destroy(gameObject, 2f);
    }
    void OnDestroy()
    {
        EventManager.instance.OnDoorOpenTrigger -= OnDoorOpenTrigger;
    }
}
