using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private ITraitEffect[] traitEffects;
    int damage;
    public void Initialize(int damage)
    {
        this.damage = damage;
        traitEffects = GetComponentsInChildren<ITraitEffect>();
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy")&& !collider.isTrigger)
        {
            for (int i = 0; i < traitEffects.Length; i++)
            {
                traitEffects[i].StartEffect(collider.gameObject);
            }
            collider.gameObject.GetComponent<CurrentHealth>().TakeDamage(damage);
        }
        if (!collider.CompareTag("Player")&& !collider.CompareTag(gameObject.tag)&& !collider.isTrigger)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject,5f);
        }
            
    }
}
