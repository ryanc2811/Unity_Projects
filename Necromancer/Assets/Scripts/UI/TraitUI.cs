using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TraitUI : MonoBehaviour
{
    public GameObject traitMenu;
    private LevelSystemAnimated levelSystem;
    private TextMeshProUGUI levelNumTxt;
    private PlayerTraits playerTraits;
    private TraitDatabase tdb;
    private Transform slotParent;
    public GameObject traitSlotPrefab;
    private List<TraitSlot> traitSlots = new List<TraitSlot>();
    private void Awake()
    {
        levelNumTxt = traitMenu.transform.Find("LevelBanner").Find("LevelNumber").Find("LevelText").GetComponent<TextMeshProUGUI>();
        slotParent = traitMenu.transform.Find("ChooseTraitMenu").Find("Content");
    }
    private void Start()
    {
        tdb = TraitDatabase.instance;
        EventManager.instance.OnTraitChosenTrigger += TraitChosen;
    }
    private void SetLevelNumber(int level)
    {
        levelNumTxt.text = "LEVEL " + (level + 1);
    }
    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystem)
    {
        this.levelSystem = levelSystem;
        SetLevelNumber(levelSystem.GetLevel());
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        traitMenu.SetActive(true);
        SetLevelNumber(levelSystem.GetLevel());
        if(traitSlots.Count<3)
            GenerateTraits();

    }
    public void SetPlayerTraits(PlayerTraits traits)
    {
        playerTraits = traits;
    }
    private void GenerateTraits()
    {
        GameState.instance.PauseGame();
        List<Trait> traits = tdb.Traits;
        for (int i = 0; i < 3; i++)
        {
            Trait trait = traits[Random.Range(0, traits.Count)];
            GameObject traitSlotObj = Instantiate(traitSlotPrefab, slotParent);
            TraitSlot traitSlot = traitSlotObj.GetComponent<TraitSlot>();
            traitSlots.Add(traitSlot);
            traitSlot.AddTrait(trait);
        }
    }
    void RemoveTraits()
    {
        for (int i = 0; i < traitSlots.Count; i++)
        {
            traitSlots[i].Remove();
        }
        traitSlots.Clear();
    }
    void TraitChosen(Trait trait)
    {
        if (traitSlots.Count > 0)
        {
            playerTraits.UnlockTrait(trait);
            RemoveTraits();
            traitMenu.SetActive(false);
        }
        GameState.instance.UnPauseGame();
    }
    void OnDestroy()
    {
        EventManager.instance.OnTraitChosenTrigger -= TraitChosen;
        levelSystem.OnLevelChanged -= LevelSystem_OnLevelChanged;
    }
}
