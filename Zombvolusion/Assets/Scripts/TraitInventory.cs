using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitInventory : MonoBehaviour
{
    public List<Trait> traits = new List<Trait>();
    
    void Awake()
    {
        traits.Clear();
    }
    public void AddTrait(Trait trait)
    {
        traits.Add(trait);
    }
    public List<Trait> GetUniqueTraits()
    {
        List<Trait> uniqueTraits = new List<Trait>();
        for (int i = 0; i < traits.Count; i++)
        {
            if (traits[i].unique)
            {
                uniqueTraits.Add(traits[i]);
            }
        }
        return uniqueTraits;
    }
}
