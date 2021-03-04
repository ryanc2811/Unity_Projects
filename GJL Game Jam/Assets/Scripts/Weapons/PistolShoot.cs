using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShoot : MonoBehaviour,IWeapon
{
    [SerializeField]
    private string animationKey;

    [SerializeField]
    private float fireRate, inputTimer, attackDamage,bulletSpeed;

    private float lastAttackTime;
    
    [SerializeField]
    private Transform attack1HitBox;
    [SerializeField]
    private LayerMask whatIsDamageable;
    private float lastInputTime=Mathf.NegativeInfinity;

    [SerializeField]
    private float bulletAliveTime;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform shootPoint;
    
    [SerializeField]
    private PlayerPlatformerController playerController;
    private Animator animator;
    public bool IsActive { get; set; }
    
    public string AnimationKey => animationKey;

    private bool gotInput,isAttacking,isFirstAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        animator.SetBool("canAttack", IsActive);
    }

    public void FinishAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);

        animator.SetBool("attack1", false);

    }

    private void CheckAttacks()
    {
        if (gotInput)
        {
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                
                isFirstAttack = !isFirstAttack;
                animator.SetBool("attack1", true);
                animator.SetBool("firstAttack", isFirstAttack);
                animator.SetBool("isAttacking", isAttacking);
            }
                
        }
        if (Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        bullet.GetComponent<IBullet>().Fire(playerController.moveDirection, bulletSpeed, attackDamage);
        Destroy(bullet, bulletAliveTime);
    }

    void CheckCombatInput()
    {
        if (Input.GetButton("Fire1"))
        {
            if (IsActive)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }
    
    public void CheckAttackHitBox()
    {
        Shoot();
    }
}
