using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpells : MonoBehaviour
{
    public int maxSpells = 3;
    public List<Spell> activeSpells = new List<Spell>();
    //Used for checking if the inventory is full
    public bool isSpellsFull = false;
    public Spell defaultSpell;
    public static ActiveSpells Instance;
    bool trigger = false;
    void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
        
    }
    void Start()
    {
    }
    void Update()
    {
        if (!trigger)
        {
            trigger = true;
            AddSpell(defaultSpell);
        }
    }
    /// <summary>
    /// ADDS AN ITEM TO THE INVENTORY
    /// </summary>
    /// <param name="spell"></param>
    public void AddSpell(Spell spell)
    {
        bool itemAdded = false;
        //IF THERE IS SPACE
        if (maxSpells > activeSpells.Count)
        {
            //ADD ITEM TO INVENTORY    
            activeSpells.Add(spell);
            itemAdded = true;
            Debug.Log(spell.name + " was added");
            isSpellsFull = false;
            EventManager.instance.SpellChangedTrigger();
        }
        //INVENTORY IS FULL
        if (!itemAdded)
        {
            Debug.Log("Cannot add " + spell.name + " inventory is full");
            isSpellsFull = true;
        }
    }
}
