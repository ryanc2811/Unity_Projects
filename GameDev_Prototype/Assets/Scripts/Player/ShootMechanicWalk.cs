using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMechanicWalk : MonoBehaviour
{
    float yMovement;
    private Rigidbody2D rb;
    float speed = .5f;
    public GameObject stepPrefab;
    float stepDelay = 1f;
    float lastStep;
    public AudioSource stepSound;
    public Transform enemy;
    private Vector3 startPosition;
    [SerializeField]private AudioSource warningSound;
    float detection;
    float lastWarningSound;
    public float soundDelay=.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        yMovement = Input.GetAxis("Vertical");
        if (yMovement!=0)
        {
            if (Time.time > lastStep + stepDelay)
            {
                lastStep = Time.time;
                SpawnFootStep();
            }
            if (detection < 1f)
                detection += 0.003f;
            else
                detection = 1f;
        }
        else
        {
            if (detection > 0)
                detection -= 0.005f;
            else
                detection = 0f;
        }

        if (detection > 0.7f)
        {
            if (Time.time > lastWarningSound + soundDelay&&!warningSound.isPlaying)
            {
                lastWarningSound = Time.time;
                warningSound.Play();
            }
        }
            
        else
            warningSound.Stop();
        Debug.Log(detection);
    }
    float GetDistanceFromEnemy()
    {
       float distEnemy = Vector3.Distance(enemy.position,transform.position);
       return distEnemy;
    }
    void SpawnFootStep()
    {
        GameObject step=Instantiate(stepPrefab,transform.position,Quaternion.identity);
        Destroy(step, 0.1f);
        stepSound.Play();
        
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector3(0, yMovement * speed);
    }
}
