using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitEffects : MonoBehaviour
{
    Stats stats;
    public GameObject attackPrefab;
    private List<GameObject> traitEffects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        stats=GetComponent<Stats>();
        EventManager.instance.OnTraitAddedTrigger += TraitUnlocked;
        EventManager.instance.OnTraitRemovedTrigger += TraitRemoved;
    }
    
    public void TraitUnlocked(Trait trait)
    {
        if (trait is AttributeAffectingTrait)
        {
            AttributeAffectingTrait tempTrait = (AttributeAffectingTrait)trait;
            stats.SetValue(tempTrait.alteredAttribute, tempTrait.value);
        }
        else
        {
            GameObject traitEffect = ((UniqueTrait)trait).effect;
            traitEffects.Add(traitEffect);
            Debug.Log("aggagha");
        }
    }
    public void TraitRemoved(Trait trait)
    {
        if (trait is AttributeAffectingTrait)
        {
            AttributeAffectingTrait tempTrait = (AttributeAffectingTrait)trait;
            stats.SetValue(tempTrait.alteredAttribute, -tempTrait.value);
        }
        else
        {
            GameObject traitEffect = ((UniqueTrait)trait).effect;
            traitEffects.Remove(traitEffect);
        }
    }
    public void Attack(Vector3 direction)
    {
        GameObject tempAttack=Instantiate(attackPrefab, transform.position, transform.rotation, null);
        for (int i = 0; i < traitEffects.Count; i++)
        {
            Instantiate(traitEffects[i], tempAttack.transform);
        }
        tempAttack.GetComponent<Attack>().Initialize((int)stats.GetValue(AttributeType.AttackDamage));
        tempAttack.GetComponent<Rigidbody>().AddForce(-direction * 50f,ForceMode.Impulse);
    }
    
    void OnDestroy()
    {
        EventManager.instance.OnTraitAddedTrigger -= TraitUnlocked;
        EventManager.instance.OnTraitRemovedTrigger -= TraitRemoved;
    }
}
