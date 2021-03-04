using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class TraitSlot : MonoBehaviour
{
    public Trait trait { get; private set; }
    private TextMeshProUGUI nameTxt;
    private Image image;
    public bool traitChosen { get; private set; }
    void Awake()
    {
        nameTxt = transform.Find("TraitName").GetComponent<TextMeshProUGUI>();
        //transform.Find("TraitImage").GetComponent<Button_UI>().ClickFunc=()=>ChooseTrait();
        image = GetComponentInChildren<Image>();
    }
    public void AddTrait(Trait newTrait)
    {
        trait = newTrait;
        nameTxt.text = trait.name;
        image.sprite = trait.image;
    }
    public void ChooseTrait()
    {
        EventManager.instance.TraitChosenTrigger(trait);
    }
    public void Remove()
    {
        Destroy(gameObject);
    }
}
