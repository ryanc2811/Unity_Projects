using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    int lvl;
    int xp;

    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;
    public LevelSystem()
    {
        lvl = 0;
        xp = 0;
        //PlayerPrefs.SetInt("Level", 0);
        //PlayerPrefs.SetInt("XP", 0);
        lvl = PlayerPrefs.GetInt("Level");
        xp = PlayerPrefs.GetInt("XP");
        
    }
    public void AddExperience(int experience)
    {
        xp += experience;
        
        while (xp >= GetXpRequired(lvl))
        {
            xp -= GetXpRequired(lvl);
            lvl++;
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }
    public int GetLevel()
    {
        return lvl;
    }
    public int GetExperience()
    {
        return xp;
    }
    public int GetXpRequired(int level)
    {
        return level+1*150;
    }
    public float GetXpNormalised()
    {
        return (float)xp / GetXpRequired(lvl);
    }
}
