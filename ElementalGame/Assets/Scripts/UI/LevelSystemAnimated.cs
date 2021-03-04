using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class LevelSystemAnimated
{
    private PlayerLevel playerLevel;
    private bool isAnimating = false;
    int lvl;
    int xp;
    private float updateTimer;
    private float updateTimerMax;

    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    public LevelSystemAnimated(PlayerLevel playerLevel)
    {
        SetPlayerLevel(playerLevel);
        FunctionUpdater.Create(() => Update());
        updateTimerMax = .016f;
    }
    private void SetPlayerLevel(PlayerLevel playerLevel)
    {
        this.playerLevel = playerLevel;
        lvl = playerLevel.GetLevel();
        xp = playerLevel.GetExperience();
        playerLevel.OnExperienceChanged += PlayerLevel_OnExperienceChanged;
        playerLevel.OnLevelChanged += PlayerLevel_OnLevelChanged;
    }

    private void PlayerLevel_OnLevelChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }

    private void PlayerLevel_OnExperienceChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }
    private void Update()
    {
        if (isAnimating)
        {
            updateTimer += Time.deltaTime;
            while (updateTimer > updateTimerMax)
            {
                updateTimer -= updateTimerMax;
                UpdateAddExperience();
            }
        }
    }
    private void UpdateAddExperience()
    {
        if (lvl < playerLevel.GetLevel())
        {
            AddExperience();
        }
        else
        {
            if (xp < playerLevel.GetExperience())
            {
                AddExperience();
            }
            else
            {
                isAnimating = false;
            }
        }
    }
    private void AddExperience()
    {
        xp++;
        if (xp >= playerLevel.GetXpRequired(lvl))
        {
            lvl++;
            xp = 0;
            EventManager.instance.LevelChangedTrigger();
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }
    public int GetLevel()
    {
        return lvl;
    }
    public float GetXpNormalised()
    {
        return (float)xp / playerLevel.GetXpRequired(lvl);
    }
    
}
