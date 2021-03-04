using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundLineSpawner:MonoBehaviour
{
    private Rigidbody2D rb;
    float timeSinceLastStep;
    [SerializeField] private float stepDelay = 1f;
    //[SerializeField] private GameObject footStepPrefab;
    [SerializeField] private AudioSource leftStepSound;
    [SerializeField] private AudioSource rightStepSound;
    [SerializeField] private SoundLineSpawner lineSpawner;


    private bool isLeftFoot = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != Vector2.zero)
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
