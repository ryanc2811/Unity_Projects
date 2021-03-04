using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetEnemy : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public GameObject currentTarget { get; private set; }
    private NavMeshAgent agent;
    
    public EntityType targetType;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        EventManager.instance.OnAIDeathTrigger += RemoveTarget;
        EventManager.instance.OnPlayerDeathTrigger += RemoveTarget;
    }
    void RemoveDeadObjects()
    {
        if (targets != null)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i])
                {
                    if (targets[i].GetComponent<ReceiveDamage>().IsDead)
                    {
                        targets.Remove(targets[i]);
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        agent.speed = GetComponent<Attributes>().moveSpeed;
        RemoveDeadObjects();
        TargetEntity();
        FindClosestEnemy();
    }
    void RemoveTarget(GameObject target)
    {
        if (targets.Contains(target))
        {
            targets.Remove(target);
        }
    }
    void FindClosestEnemy()
    {
        if (targets!=null)
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
        
    }
    void TargetEntity()
    {
        if (currentTarget)
        {
            if (Vector3.Distance(transform.position, currentTarget.transform.position) > 5f)
            {
                agent.SetDestination(currentTarget.transform.position);
                transform.LookAt(currentTarget.transform);
            }
        }
    }
    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("Enemy") || collider.CompareTag("Turned"))
        {
            EntityType entityType = collider.gameObject.GetComponent<Attributes>().entityType;
            if (entityType == targetType)
            {
                if (!targets.Contains(collider.gameObject))
                    targets.Add(collider.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("Enemy") || collider.CompareTag("Turned"))
        {
            EntityType entityType = collider.gameObject.GetComponent<Attributes>().entityType;
            if (entityType == targetType)
            {
                targets.Remove(collider.gameObject);
                if (currentTarget == collider.gameObject)
                    currentTarget = null;
            }
        }
    }
    void OnDestroy()
    {
        EventManager.instance.OnAIDeathTrigger -= RemoveTarget;
        EventManager.instance.OnPlayerDeathTrigger -= RemoveTarget;
    }
}
