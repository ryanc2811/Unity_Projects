using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlot : MonoBehaviour
{
    /// <summary>
    /// SELECTS AN ITEM IN THE INVENTORY
    /// </summary>
    public void OnSpellSelect()
    {
        EventManager.instance.SpellSelectedTrigger(gameObject);
    }
}
