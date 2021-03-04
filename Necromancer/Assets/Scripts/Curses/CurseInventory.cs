using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseInventory : MonoBehaviour
{
    public List<Curse> curses = new List<Curse>();
    Stats stats;
    void Awake()
    {
        curses.Clear();
        stats = GetComponent<Stats>();
    }
    public void UnlockCurse(Curse curse)
    {
        
        if (gameObject.CompareTag("Player"))
        {
            if (curse is AttributeAffectingCurse)
            {
                curses.Add(curse);
                AttributeAffectingCurse tempCurse = (AttributeAffectingCurse)curse;
                stats.SetValue(tempCurse.alteredAttribute, tempCurse.value);
            }
            else
            {
                if (!IsCurseUnlocked(curse))
                {
                    curses.Add(curse);
                    ((UniqueCurse)curse).Initialize(gameObject);
                }
            }
        }
        else
        {
            curses.Add(curse);
        }
    }
    public void RemoveCurse(Curse curse)
    {
        if (IsCurseUnlocked(curse))
        {
            curses.Remove(curse);
            if (gameObject.CompareTag("Player"))
            {
                if (curse is AttributeAffectingCurse)
                {
                    AttributeAffectingCurse tempCurse = (AttributeAffectingCurse)curse;
                    stats.SetValue(tempCurse.alteredAttribute, -tempCurse.value);
                }
                else
                {
                    ((UniqueCurse)curse).RemoveCurse();
                }
            }
        }
    }
    public bool IsCurseUnlocked(Curse curse)
    {
        return curses.Contains(curse);
    }
    public List<Curse> GetUniqueTraits()
    {
        List<Curse> uniqueTraits = new List<Curse>();
        for (int i = 0; i < curses.Count; i++)
        {
            if (curses[i] is UniqueCurse)
            {
                uniqueTraits.Add(curses[i]);
            }
        }
        return uniqueTraits;
    }
}
