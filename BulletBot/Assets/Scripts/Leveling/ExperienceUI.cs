using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ExperienceUI : MonoBehaviour
{
    LevelSystem playerLevel;
    private LevelSystemAnimated levelSystemAnimated;
    TextMeshProUGUI levelNumTxt;
    Image experienceBarImage;
    // Start is called before the first frame update
    void Awake()
    {
        levelNumTxt = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        experienceBarImage = transform.Find("ExperienceBar").GetComponent<Image>();
        
    }
    private void SetExperienceBarSize(float xp)
    {
        experienceBarImage.fillAmount = xp;
    }
    private void SetLevelNumber(int level)
    {
        levelNumTxt.text = "LEVEL " + (level + 1);
    }
    public void SetPlayerLevel(LevelSystem pl)
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
