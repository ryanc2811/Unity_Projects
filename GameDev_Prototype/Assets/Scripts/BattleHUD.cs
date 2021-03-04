using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Slider healthBar;

    public void SetHUD(Unit unit)
    {
        healthBar.maxValue = unit.GetMaxHealth();
        healthBar.value = unit.GetCurrentHealth();
    }

    public void SetHP(int hp)
    {
        healthBar.value = hp;
    }
}
