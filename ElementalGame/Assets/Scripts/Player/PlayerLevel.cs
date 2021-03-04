using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel
{
    int lvl;
    int xp;

    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;
    public PlayerLevel()
    {
        lvl = 0;
        xp = 0;
    }
    public void AddExperience(int experience)
    {
        xp += experience;
        EventManager.instance.ExperienceChangedTrigger();
        while (xp >= GetXpRequired(lvl))
        {
            xp -= GetXpRequired(lvl);
            lvl++;
            EventManager.instance.LevelChangedTrigger();
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
        return level+1*100;
    }
    public float GetXpNormalised()
    {
        return (float)xp / GetXpRequired(lvl);
    }
}
