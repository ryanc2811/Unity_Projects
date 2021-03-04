using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetEnemy : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public GameObject currentTarget { get; private set; }
    

    public List<EntityType> targetTypes = new List<EntityType>();
    void Start()
    {
        
        EventManager.instance.OnAIDeathTrigger += RemoveTarget;
    }
    
    // Update is called once per frame
    void Update()
    {
        for (var i = targets.Count - 1; i > -1; i--)
        {
            if (targets[i] == null)
                targets.RemoveAt(i);
        }
        FindClosestEnemy();
    }
    void RemoveTarget(GameObject target)
    {
        if (targets.Contains(target))
        {
            targets.Remove(target);
            if (currentTarget == target)
                currentTarget = null;
        }
    }
    void FindClosestEnemy()
    {
            if (targets.Count > 1)
            {
                for (int i = 0; i < targets.Count - 1; i++)
                {
                    for (int j = i + 1; j < targets.Count; j++)
                    {
                        float distanceFromA = Vector3.Distance(transform.position, targets[i].transform.position);
                        float distanceFromB = Vector3.Distance(transform.position, targets[j].transform.position);
                        if (distanceFromA < distanceFromB)
                        {
                            currentTarget = targets[i];
                        }
                        else
                        {
                            currentTarget = targets[j];
                        }
                    }
                }
            }
            else if(targets.Count==1)
            {
                currentTarget = targets[0];
            }
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("Enemy") || collider.CompareTag("Turned"))
        {
            EntityType entityType = collider.gameObject.GetComponent<Stats>().entityType;
            if (targetTypes.Contains(entityType))
            {
                if (!targets.Contains(collider.gameObject))
                    targets.Add(collider.gameObject);
            }
        }
    }
    void OnDestroy()
    {
        EventManager.instance.OnAIDeathTrigger -= RemoveTarget;
        EventManager.instance.OnPlayerDeathTrigger -= RemoveTarget;
    }
}
