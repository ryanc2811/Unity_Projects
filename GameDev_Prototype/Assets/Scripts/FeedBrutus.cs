using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBrutus : MonoBehaviour
{

    Brutus brutus;
    public LayerMask obstacleMask;
    public LayerMask targetMask;

    [SerializeField]
    AIPath path;
    [SerializeField]
    public float viewRadius;
    [SerializeField]
    [Range(0, 360)]
    public float viewAngle;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    private Transform lastVisibleTarget;
    private bool eventLaunched;

    private bool targetFound = false;

    private Brutus.State previousState;

    private bool stateChecked = false;
    private bool startedEating = false;
    // Start is called before the first frame update
    void Start()
    {
        brutus = GetComponent<Brutus>();
    }


    void CheckForFood()
    {
        visibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            if (Vector2.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);
                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    targetFound = true;
                    lastVisibleTarget = target;
                    eventLaunched = false;
                }
            }
            else
            {
                if (!eventLaunched)
                {
                    targetFound = false;
                    lastVisibleTarget = null;
                    eventLaunched = true;
                }
            }
        }
        if (targetsInViewRadius.Length == 0 && !eventLaunched)
        {
            targetFound = false;
            lastVisibleTarget = null;
            eventLaunched = true;
        }
    }


    public Vector3 DirFromAngles(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        float angleRad = angleInDegrees * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
    // Update is called once per frame
    void Update()
    {
        CheckForFood();

        if (lastVisibleTarget != null)
        {
            if (!stateChecked)
            {
                stateChecked = true;
                previousState = brutus.currentState;
            }
            brutus.currentState = Brutus.State.Feeding;
            path.destination = lastVisibleTarget.position;

            if (Vector3.Distance(lastVisibleTarget.position, transform.position) < 1f)
            {
                if (!startedEating)
                {
                    StartCoroutine(EatMeat());
                    startedEating = true;
                }
            }
        }
    }
    private void FinishEating()
    {
        StopCoroutine(EatMeat());
        if (previousState != Brutus.State.Raging)
        {
            brutus.GainHappiness();
            brutus.currentState = Brutus.State.Happy;
        }
        else
        {
            brutus.currentState = Brutus.State.Raging;
        }
        stateChecked = false;
        startedEating = false;
    }
    IEnumerator EatMeat()
    {
        Destroy(lastVisibleTarget.gameObject, 1.5f);
        yield return new WaitForSeconds(1.5f);
        FinishEating();
           
    }
}
