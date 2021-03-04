using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSelector : MonoBehaviour
{
    public SpellCooldown[] slots { get; private set; }
    public void Start()
    {
        slots = GetComponentsInChildren<SpellCooldown>();
        EventManager.instance.OnSpellSelectedTrigger += SpellSelected;

    }
    private void SpellSelected(GameObject spell)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].gameObject == spell)
            {
                slots[i].SpellSelected(true);
            }
            else
            {
                slots[i].SpellSelected(false);
            }
        }
    }
    void OnDestroy()
    {
        EventManager.instance.OnSpellSelectedTrigger -= SpellSelected;
    }
}
