using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ZombieSlot : MonoBehaviour
{
    public TextMeshProUGUI traitName;
    public TextMeshProUGUI traitDescription;
    public Image traitImage;
    public Slider zombieHealth;
    public GameObject zombie;
    public Curse curse;
    public bool slotPressed { get; private set; }
    public float currentHealth;
    public void AddZombie(GameObject pZombie)
    {
        zombie = pZombie;
        zombieHealth.enabled = true;
        traitImage.enabled = true;
        traitDescription.enabled = true;
        traitName.enabled = true;
        zombieHealth.maxValue = zombie.GetComponent<Stats>().GetValue(AttributeType.MaxHealth);
        zombieHealth.value = zombie.GetComponent<CurrentHealth>().health;
        currentHealth= zombie.GetComponent<CurrentHealth>().health;
        curse = zombie.GetComponent<CurseInventory>().curses[0];
        traitImage.sprite = curse.image;
        traitDescription.text = curse.description;
        traitName.text = curse.name;
    }
    public void RemoveZombie()
    {
        curse = null;
        zombieHealth.value = 0;
        zombieHealth.enabled = false;
        traitImage.sprite = null;
        traitImage.enabled = false;
        traitDescription.text = null;
        traitDescription.enabled = false;
        traitName.text = null;
        traitName.enabled = false;
        EventManager.instance.AIDeathTrigger(zombie);
        Destroy(zombie);
        
    }
    public void ButtonPressed()
    {
        slotPressed = true;
    }
}
