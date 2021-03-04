using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseEffects : MonoBehaviour
{
    CurseInventory curseInventory;
    
    // Start is called before the first frame update
    void Awake()
    {
        curseInventory = GetComponent<CurseInventory>();
    }
  
    // Update is called once per frame
    void Update()
    {
        List<Curse> uniqueCurses = curseInventory.GetUniqueTraits();

        if (uniqueCurses.Count > 0)
        {
            for (int i = 0; i < uniqueCurses.Count; i++)
            {
                UniqueCurse curse =(UniqueCurse)uniqueCurses[i];
                curse.Curse();
            }
        }
    }
}
