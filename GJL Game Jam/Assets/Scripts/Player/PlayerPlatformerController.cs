using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
  
    
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    public float hangTime = .2f;
    private float hangCounter;

    public float jumpBufferLength=.01f;
    private float jumpBufferCount;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public ParticleSystem footSteps;
    private ParticleSystem.EmissionModule footEmission;

    public ParticleSystem impactEffect;
    private bool wasOnGround = false;
    private bool canFlip = true;
    public float moveDirection { get; private set; }
    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
        footEmission = footSteps.emission;
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        
        move.x = Input.GetAxis("Horizontal");


        //Manage Jump Buffer
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCount = jumpBufferLength;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }

        if (jumpBufferCount>0f&&grounded)
        {
            velocity.y = jumpTakeOffSpeed;
            animator.SetTrigger("jumped");
            jumpBufferCount = 0;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        //show Footstep effect
        if (Input.GetAxisRaw("Horizontal") != 0 && grounded)
            footEmission.rateOverTime = 35f;
        else
            footEmission.rateOverTime = 0f;

        //Show impact effect
        if (!wasOnGround && grounded)
        {
            animator.ResetTrigger("jumped");
            impactEffect.gameObject.SetActive(true);
            impactEffect.Stop();
            impactEffect.transform.position = footSteps.transform.position;
            impactEffect.Play();
        }

        wasOnGround = grounded;

        if(Input.GetAxisRaw("Horizontal") != 0&&canFlip)
        {
            bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
            
            if (flipSprite)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }

        if (spriteRenderer.flipX)
        {
            moveDirection = -1;
        }
        else
        {
            moveDirection = 1;
        }
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    void DisableFlip()
    {
        canFlip = false;
    }
    void EnableFlip()
    {
        canFlip = true;
    }
}