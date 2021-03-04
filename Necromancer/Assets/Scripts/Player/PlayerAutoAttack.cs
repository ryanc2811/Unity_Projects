using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoAttack : MonoBehaviour
{
    Stats stats;
    float attackDelay;
    int attackDamage;
    GameObject attackTarget;
    private List<GameObject> targets = new List<GameObject>();
    float nextAttack;
    private TraitEffects traitEffects;
    private PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponentInParent<Stats>();
        controller = PlayerController.instance;
        traitEffects = GetComponentInParent<TraitEffects>();
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
        AttackEnemy();
    }
    void AttackEnemy()
    {
        attackDelay = stats.GetValue(AttributeType.AttackDelay);
        attackDamage = (int)stats.GetValue(AttributeType.AttackDamage);

        if (attackTarget!=null&&!controller.isMoving())
        {
            if (nextAttack + attackDelay < Time.time)
            {
                nextAttack = Time.time;
                
                Vector3 heading = transform.position - attackTarget.transform.position;
                float distance = heading.magnitude;
                Vector3 direction = heading/distance;
                transform.parent.rotation = Quaternion.LookRotation(-direction);
                traitEffects.Attack(direction);
            }
        }
    }
    void RemoveTarget(GameObject target)
    {
        if (targets.Contains(target))
        {
            targets.Remove(target);
            if (attackTarget == target)
                attackTarget = null;
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
                        attackTarget = targets[i];
                    }
                    else
                    {
                        attackTarget = targets[j];
                    }
                }
            }
        }
        else if (targets.Count == 1)
        {
            attackTarget = targets[0];
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy") && !collider.isTrigger)
        {
            if (!targets.Contains(collider.gameObject))
                targets.Add(collider.gameObject);
        }
    }
    void OnDestroy()
    {
        EventManager.instance.OnAIDeathTrigger -= RemoveTarget;
    }
}
