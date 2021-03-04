using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToRelic : IState
{
    //private IEnemyMovement enemy;
    private GameObject relic;
    private NavMeshAgent agent;
    private Transform edgeOfScreen;
    float distanceFromRelic;
    float minDistance = 3f;
    public bool ReachedRelic { get; private set; }
    bool trigger = false;
    public MoveToRelic(GameObject gameObject)
    {
        //enemy = gameObject.GetComponent<EnemyMovement>();
        relic = GameObject.FindGameObjectWithTag("Relic");
        agent=gameObject.GetComponent<NavMeshAgent>();
        edgeOfScreen = GameObject.FindGameObjectWithTag("EdgeOfScreen").transform;
    }
    public void Enter()
    {
        //start animation
        Animate();
        //enemy.MoveToRelic();
        agent.SetDestination(relic.transform.position);
    }

    public void Execute()
    {
        distanceFromRelic = Vector3.Distance(relic.transform.position, agent.transform.position);
        if (distanceFromRelic <= minDistance&&!ReachedRelic)
        {
            ReachedRelic = true;
        }
        if (agent.GetComponent<Renderer>().isVisible&&ReachedRelic)
        {
            if (!trigger)
            {
                relic.SetActive(false);
                agent.SetDestination(new Vector3(edgeOfScreen.position.x,agent.transform.position.y,edgeOfScreen.position.z));
                
                trigger = true;
                Debug.Log(agent.destination);
            }
        }
    }

    private void Animate()
    {
        
    }
    public void Exit()
    {

    }
}
