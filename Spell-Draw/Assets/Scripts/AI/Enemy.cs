using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private IStateMachine stateMachine;
    private int health = 100;
    private GameObject relic;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine();
        relic = GameObject.FindGameObjectWithTag("Relic");
    }
    /// <summary>
    /// A method for requesting the state to change to the given parameter
    /// </summary>
    /// <param name="requestedState"></param>
    public void RequestChangeState(IState requestedState)
    {
        //change state
        stateMachine.ChangeCurrentState(requestedState);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            RequestChangeState(new MoveToRelic(this.gameObject));
        if (Input.GetKeyDown(KeyCode.F))
        {
            health -= 50;
        }

        if (stateMachine.CurrentState is MoveToRelic && health <= 0)
        {
            if (((MoveToRelic)stateMachine.CurrentState).ReachedRelic)
            {
                relic.SetActive(true);
                relic.transform.position = new Vector3(transform.position.x, relic.transform.position.y, transform.position.z);
            }
        }
        stateMachine.Update();
        if (health <= 0)
        {
            Destroy(gameObject);
            CenterCamera.instance.LockCameraToTarget();
        }
    }
}
