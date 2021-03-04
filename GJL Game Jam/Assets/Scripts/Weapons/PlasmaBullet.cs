using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBullet : MonoBehaviour, IBullet
{
    private Rigidbody2D rb;
    private float damage;
    private MorphType damageType=MorphType.Plasma;
    [SerializeField]
    private GameObject hitEffect;

    private bool hasHitObject = false;
    public void Fire(float xDirection, float bulletSpeed, float pDamage)
    {
        rb = GetComponent<Rigidbody2D>();
        damage = pDamage;
        rb.AddForce(new Vector2(xDirection * bulletSpeed, 0),ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Damageable"))
        {
            other.gameObject.GetComponentInParent<CurrentHealth>().RequestDamage(damageType, damage);
        }
        if (!other.CompareTag("Player") && !other.CompareTag("Projectile")&&!hasHitObject)
            DestroyBullet();
    }
    void DestroyBullet()
    {
        hasHitObject = true;
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, .4f);
        Destroy(gameObject);
    }
}
