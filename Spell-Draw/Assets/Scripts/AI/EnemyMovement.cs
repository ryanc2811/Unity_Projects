using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour,IEnemyMovement
{
    NavMeshAgent navigation;
    bool targetRelic=false;
    bool reachedRelic = false;
    Rigidbody rb;
    GameObject relic;
    float distanceFromRelic;
    float minDistance = 3f;
    float speed = 10f;
    public bool RelicCarrier { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        relic = GameObject.FindGameObjectWithTag("Relic");
    }
    public void MoveToRelic()
    {
        targetRelic = true;
    }
    public bool ReachedRelic()
    {
        if (reachedRelic)
        {
            relic.SetActive(false);
            RelicCarrier = true;
        }
        return reachedRelic;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (targetRelic&&relic)
        {
            distanceFromRelic = Vector3.Distance(relic.transform.position, transform.position);
            if (distanceFromRelic > minDistance)
            {
                Vector3 direction = (relic.transform.position - transform.position).normalized;
                rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
                reachedRelic = false;
            }
            else
            {
                targetRelic = false;
                reachedRelic = true;
            }
        }
    }
}
