using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundLineSpawner:MonoBehaviour
{
    private AIPath ai;
    float timeSinceLastStep;
    [SerializeField] private float stepDelay = 1f;
    [SerializeField] private GameObject footStepPrefab;
    [SerializeField] private AudioSource leftStepSound;
    [SerializeField] private AudioSource rightStepSound;
    [SerializeField] private SoundLineSpawner lineSpawner;

    public Brutus brutus;
    private bool isLeftFoot = false;
    void Start()
    {
        ai = GetComponent<AIPath>();
    }
    // Update is called once per frame
    void Update()
    {

        if (ai.velocity != Vector3.zero&&brutus.currentState==Brutus.State.Raging)
        {
            if (Time.time > timeSinceLastStep + stepDelay)
            {
                timeSinceLastStep = Time.time;
                lineSpawner.SpawnLines();
                //GameObject footStep = Instantiate(footStepPrefab, transform.position, Quaternion.identity);

                if (isLeftFoot)
                    leftStepSound.Play();
                else
                    rightStepSound.Play();
                isLeftFoot = !isLeftFoot;
                //Destroy(footStep, 2f);
            }
        }
    }
}
