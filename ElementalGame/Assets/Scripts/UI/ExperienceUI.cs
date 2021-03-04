using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceUI : MonoBehaviour
{
    PlayerLevel playerLevel;
    private LevelSystemAnimated levelSystemAnimated;
    Text levelNumTxt;
    Image experienceBarImage;
    // Start is called before the first frame update
    void Awake()
    {
        levelNumTxt = transform.Find("LevelText").GetComponent<Text>();
        experienceBarImage = transform.Find("ExperienceBar").GetComponent<Image>();
        
    }
    private void SetExperienceBarSize(float xp)
    {
        experienceBarImage.fillAmount = xp;
    }
    private void SetLevelNumber(int level)
    {
        levelNumTxt.text = "LEVEL\n" + (level + 1);
    }
    public void SetPlayerLevel(PlayerLevel pl)
    {
        playerLevel = pl;
    }
    public void SetLevelSystemAnimated(LevelSystemAnimated LSA)
    {
        levelSystemAnimated = LSA;
        SetLevelNumber(levelSystemAnimated.GetLevel());
        SetExperienceBarSize(levelSystemAnimated.GetXpNormalised());
        levelSystemAnimated.OnExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;
    }

    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(levelSystemAnimated.GetLevel());
    }

    private void LevelSystemAnimated_OnExperienceChanged(object sender, System.EventArgs e)
    {
        SetExperienceBarSize(levelSystemAnimated.GetXpNormalised());
    }
    void OnDestroy()
    {
        levelSystemAnimated.OnExperienceChanged -= LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged -= LevelSystemAnimated_OnLevelChanged;
    }
}
