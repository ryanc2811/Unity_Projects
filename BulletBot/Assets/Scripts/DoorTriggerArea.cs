using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerArea : MonoBehaviour
{
    [SerializeField]
    private int id;
    [SerializeField]
    private List<TargetType> targets=new List<TargetType>();
    [SerializeField]
    private float closeDoorAfter = 1f;
    void OnTriggerEnter(Collider collider)
    {
        if (targets.Contains(TargetType.Player)&&collider.CompareTag("Player"))
        {
            EventManager.instance.DoorOpenTrigger(id);
            StartCoroutine(CloseDoorTimer());
        }
        if (targets.Contains(TargetType.Enemy) && collider.CompareTag("Enemy"))
        {
            EventManager.instance.DoorOpenTrigger(id);
            Debug.Log("Hi");
            StartCoroutine(CloseDoorTimer());
        }
    }

    IEnumerator CloseDoorTimer()
    {
        yield return new WaitForSeconds(closeDoorAfter);
        EventManager.instance.DoorCloseTrigger(id);
    }
}
enum TargetType
{
    Player,
    Enemy
}
