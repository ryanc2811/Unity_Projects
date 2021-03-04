using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutUI : MonoBehaviour
{
    SpellSlot[] slots;
    SpellLoadout spellLoadout;
    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<SpellSlot>();
        spellLoadout = SpellLoadout.Instance;
        spellLoadout.OnSpellAddedCallback += UpdateUI;
    }

    // Update is called once per frame
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < spellLoadout.spells.Count)
                slots[i].AddItem(spellLoadout.spells[i]);
            else
                slots[i].RemoveItem();
        }
    }
}
