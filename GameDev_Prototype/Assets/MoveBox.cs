using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    Rigidbody2D rb;
    public AudioSource moveSound;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (rb.velocity.magnitude>0.1f)
        {
            if(!moveSound.isPlaying)moveSound.Play();
        }
    }
}
