using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public PlayerLevel playerLevel;
    private ExperienceUI xpUI;
    private TraitUI traitUI;
    private ResurrectUI resurrectUI;
    private PlayerTraits playerTraits;
    LevelSystemAnimated levelSystemAnimated;
    private CurseInventory playerCurseInv;
    // Start is called before the first frame update
    void Awake()
    {
        xpUI = GameObject.FindGameObjectWithTag("LevelUI").GetComponent<ExperienceUI>();
        traitUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<TraitUI>();
        resurrectUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<ResurrectUI>();
        playerLevel = new PlayerLevel();
        levelSystemAnimated = new LevelSystemAnimated(playerLevel);
    }
    void Start()
    {
        playerTraits = PlayerController.instance.Mover.GetComponent<PlayerTraits>();
        playerCurseInv = PlayerController.instance.Mover.GetComponent<CurseInventory>();
        xpUI.SetLevelSystemAnimated(levelSystemAnimated);
        traitUI.SetLevelSystemAnimated(levelSystemAnimated);
        traitUI.SetPlayerTraits(playerTraits);
        resurrectUI.SetPlayerCurseInventory(playerCurseInv);
        EventManager.instance.OnGivePlayerExperienceTrigger+=AddXp;
    }
    public void AddXp(int xp)
    {
        playerLevel.AddExperience(xp);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
