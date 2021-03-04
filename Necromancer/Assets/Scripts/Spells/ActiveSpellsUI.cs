using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpellsUI : MonoBehaviour
{
    public Transform spellSelector;
    SpellCooldown[] slots;
    public GameObject spellSlotsUI;
    ActiveSpells spells;
    public GameObject spellHolder;
    // Start is called before the first frame update
    void Start()
    {
        spells = ActiveSpells.Instance;
        EventManager.instance.OnSpellChangedTrigger += UpdateUI;
        slots = spellSelector.GetComponentsInChildren<SpellCooldown>();
    }
    
    // Update is called once per frame
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < spells.activeSpells.Count)
            {
                slots[i].gameObject.SetActive(true);
                slots[i].Initialize(spells.activeSpells[i], spellHolder);
            }
                
            else
                slots[i].Reset();
        }
    }
    void OnDestroy()
    {
        EventManager.instance.OnSpellChangedTrigger -= UpdateUI;
    }
}
