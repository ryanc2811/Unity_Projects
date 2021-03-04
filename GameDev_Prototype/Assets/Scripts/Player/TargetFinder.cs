using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : FieldOfView
{
    public LayerMask targetMask;
    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();
    private Transform lastVisibleTarget;

    private bool eventLaunched = false;
    public delegate void OnNewTargetFound(Transform target);
    public OnNewTargetFound OnNewTargetFoundCallback;
    public delegate void OnTargetNotFound();
    public OnTargetNotFound OnTargetNotFoundCallback;
    protected override void Initialize()
    {
        StartCoroutine(FindTargetsWithDelay(.2f));
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    if (target != lastVisibleTarget)
                    {
                        if (OnNewTargetFoundCallback != null)
                            OnNewTargetFoundCallback.Invoke(target);
                    }

                    lastVisibleTarget = target;
                    eventLaunched = false;
                }
            }
            else
            {
                if (!eventLaunched)
                {
                    if (OnTargetNotFoundCallback != null)
                        OnTargetNotFoundCallback.Invoke();
                    lastVisibleTarget = null;
                    eventLaunched = true;
                }
            }
        }
        if (targetsInViewRadius.Length == 0 && !eventLaunched)
        {
            if (OnTargetNotFoundCallback != null)
                OnTargetNotFoundCallback.Invoke();
            lastVisibleTarget = null;
            eventLaunched = true;
        }
    }
}
