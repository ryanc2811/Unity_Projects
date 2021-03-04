using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDatabase : MonoBehaviour
{
    public static SpellDatabase Instance;
    public List<Spell> spells = new List<Spell>();
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public Spell GetSpell(string name)
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
