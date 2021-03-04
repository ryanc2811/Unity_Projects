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
    public bool slotPressed { get; private set; }

    public void AddZombie(GameObject pZombie)
    {
        zombie = pZombie;
        zombieHealth.enabled = true;
        traitImage.enabled = true;
        traitDescription.enabled = true;
        traitName.enabled = true;
        zombieHealth.value = zombie.GetComponent<ReceiveDamage>().health;
        Trait trait = zombie.GetComponent<TraitInventory>().traits[0];
        traitImage.sprite = trait.image;
        traitDescription.text = trait.description;
        traitName.text = trait.name;
    }
    public void RemoveZombie()
    {
        zombie = null;
        zombieHealth.value = 0;
        zombieHealth.enabled = false;
        traitImage.sprite = null;
        traitImage.enabled = false;
        traitDescription.text = null;
        traitDescription.enabled = false;
        traitName.text = null;
        traitName.enabled = false;
    }
    public void ButtonPressed()
    {
        slotPressed = true;
    }
}
