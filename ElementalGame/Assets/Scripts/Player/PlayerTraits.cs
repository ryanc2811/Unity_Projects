using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTraits : MonoBehaviour
{
    private List<Trait> traits = new List<Trait>();

    public void UnlockTrait(Trait trait)
    {
        //if (!IsTraitUnlocked(trait))
        //{
            traits.Add(trait);
            EventManager.instance.TraitAddedTrigger(trait);
        //}
    }
    public void RemoveTrait(Trait trait)
    {
        if (IsTraitUnlocked(trait))
        {
            traits.Remove(trait);
            EventManager.instance.TraitRemovedTrigger(trait);
        }
    }
    public bool IsTraitUnlocked(Trait trait)
    {
        return traits.Contains(trait);
    }
}
