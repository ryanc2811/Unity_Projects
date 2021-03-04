using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellLoadout : MonoBehaviour
{
    public int maxSpells = 3;
    public List<Spell> spells = new List<Spell>();
    public static SpellLoadout Instance;
    public delegate void OnSpellLoadoutFull(Spell spell);
    public OnSpellLoadoutFull OnSpellFullCallback;
    public delegate void OnSpellAdded();
    public OnSpellAdded OnSpellAddedCallback;
    bool trigger = false;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (!trigger)
        {
            trigger = true;
            AddSpell(SpellDatabase.Instance.GetSpell("Square"));
        }
            
    }
    public void AddSpell(Spell spell)
    {
        if (spells.Count < maxSpells)
        {
            spells.Add(spell);
            if(OnSpellAddedCallback!=null)
                OnSpellAddedCallback.Invoke();
        }
        else
        {
            OnSpellFullCallback.Invoke(spell);
        }
    }
    public Spell SpellInLoadout(string name)
    {
        for (int i = 0; i < spells.Count; i++)
        {
            if (spells[i].name == name)
            {
                return spells[i];
            }
        }
        return null;
    }
}
