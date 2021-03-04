using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFollower : MonoBehaviour
{
    TargetEnemy targetEnemy;

    private GameObject player;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        targetEnemy = GetComponent<TargetEnemy>();
        EventManager.instance.OnNewPlayerTrigger += NewPlayer;
        player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>().Mover;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetEnemy.currentTarget&&player)
        {
            if(Vector3.Distance(transform.position,player.transform.position)>15f)
                agent.SetDestination(player.transform.position);
        }
    }
    void NewPlayer(GameObject player)
    {
        this.player = player;
    }
    void OnDestroy()
    {
        EventManager.instance.OnNewPlayerTrigger -= NewPlayer;
    }
}
