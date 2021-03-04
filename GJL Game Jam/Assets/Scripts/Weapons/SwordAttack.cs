using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour,IWeapon
{
    [SerializeField]
    private string animationKey;

    [SerializeField]
    private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField]
    private Transform attack1HitBox;
    [SerializeField]
    private LayerMask whatIsDamageable;
    private float lastInputTime = Mathf.NegativeInfinity;

    [SerializeField]
    private GameObject impactEffect;

    public MorphType damageType;

    private Animator animator;
    public bool IsActive { get; set; }

    public string AnimationKey => animationKey;

    private bool gotInput, isAttacking, isFirstAttack = false;

    int attackCombo;
    float comboTimer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        animator.SetBool("canAttack", IsActive);
    }

    public void FinishAttack()
    {
        
        if (attackCombo == 1)
        {
            //record the 
            comboTimer = Time.time;

            animator.SetBool("attack1", false);
        }
        if (attackCombo == 2)
        {
            //record the 
            comboTimer = Time.time;

            animator.SetBool("attack1", false);
        }
        if (attackCombo == 3)
        {
            animator.SetBool("attack1", false);
            
        }
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
    }

    private void CheckAttacks()
    {
        //if attack input pressed
        if (gotInput)
        {
            //if not already in an attack animation
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                attackCombo++;
                animator.SetInteger("attackIndex", attackCombo);
                animator.SetBool("isAttacking", isAttacking);
            }
        }
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
    void CheckAttack1HitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBox.position, attack1Radius, whatIsDamageable);

        foreach (Collider2D collider in detectedObjects)
        {
            CurrentHealth receiveDamage = collider.transform.parent.GetComponent<CurrentHealth>();
            if (receiveDamage != null)
            {
                Debug.Log("HIT " + collider.name);
                receiveDamage.RequestDamage(damageType, attack1Radius);
            }
            GameObject impact = Instantiate(impactEffect, collider.transform.position, Quaternion.identity);
            Destroy(impact, .6f);
        }
    }
    public void CheckAttackHitBox()
    {
        switch (attackCombo)
        {
            case 1:
                CheckAttack1HitBox();
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBox.position, attack1Radius);
    }
}
