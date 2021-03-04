using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappinessUI : MonoBehaviour
{
    public Image fillImage;

    public Color green;
    public Color orange;
    public Color red;

    public void UpdateUI(int newValue,int maxValue)
    {
        float fillValue = (float)newValue / (float)maxValue;
        fillImage.fillAmount = fillValue;

        if (fillValue > 0.6)
        {
            fillImage.color = green;
        }
        if (fillValue > 0.3f && fillValue < 0.6f)
        {
            fillImage.color = orange;
        }
        if (fillValue < 0.3f)
        {
            fillImage.color = red;
        }
    }
}
