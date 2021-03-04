using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SpellSlot : MonoBehaviour
{
    private Spell spell;
    public Image Icon { get; private set; }
    public TextMeshProUGUI SpellName { get; private set; }
    void Start()
    {
        Icon = GetComponent<Image>();
        SpellName = GetComponentInChildren<TextMeshProUGUI>();
    }
    /// <summary>
    /// ADDS AN ITEM TO THE INVENTORY
    /// </summary>
    /// <param name="pItem"></param>
    public void AddItem(Spell pSpell)
    {
        spell = pSpell;
        Icon.sprite = spell.image;
        Icon.enabled = true;
        SpellName.text = spell.name;
        SpellName.enabled = true;
    }
    /// <summary>
    /// REMOVES AN ITEM FROM THE INVENTORY
    /// </summary>
    public void RemoveItem()
    {
        spell = null;
        Icon.sprite = null;
        Icon.enabled = false;
        SpellName.text = null;
        SpellName.enabled = false;
    }
}
