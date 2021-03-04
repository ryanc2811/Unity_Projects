using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    /// <summary>
    /// Set the max value of the health bar
    /// </summary>
    /// <param name="health"></param>
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color=gradient.Evaluate(1f);
    }
    /// <summary>
    /// set the current value of the health bar
    /// </summary>
    /// <param name="health"></param>
    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
