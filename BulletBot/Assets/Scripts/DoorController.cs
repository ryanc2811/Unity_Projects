using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private int id;
    [SerializeField]
    private float startY;
    [SerializeField]
    private float endY;
    [SerializeField]
    private float animationTime=1f;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnDoorOpenTrigger += OnDoorOpenTrigger;
        EventManager.instance.OnDoorCloseTrigger += OnDoorCloseTrigger;
    }

    private void OnDoorCloseTrigger(int id)
    {
        if(this.id==id)
            LeanTween.moveLocalY(gameObject, startY, animationTime).setEaseInQuad();
    }

    private void OnDoorOpenTrigger(int id)
    {
        if (this.id == id)
            LeanTween.moveLocalY(gameObject, endY, animationTime).setEaseOutQuad();
    }
    void OnDestroy()
    {
        EventManager.instance.OnDoorOpenTrigger -= OnDoorOpenTrigger;
        EventManager.instance.OnDoorCloseTrigger -= OnDoorCloseTrigger;
    }
}
