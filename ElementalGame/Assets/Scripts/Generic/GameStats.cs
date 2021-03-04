using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    private PlayerLevel playerLevel;
    private ExperienceUI xpUI;
    private TraitUI traitUI;
    private PlayerTraits playerTraits;
    private LevelSystemAnimated levelSystemAnimated;
    private CurseInventory playerCurseInv;
    // Start is called before the first frame update
    void Awake()
    {
        xpUI = GameObject.FindGameObjectWithTag("LevelUI").GetComponent<ExperienceUI>();
        traitUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<TraitUI>();
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
