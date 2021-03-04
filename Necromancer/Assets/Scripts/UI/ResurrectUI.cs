using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectUI : MonoBehaviour
{
    List<ZombieSlot> slots = new List<ZombieSlot>();

    public GameObject slotPrefab;
    public GameObject UIParent;
    public GameObject MenuObject;
    private CurseInventory playerCurseInv;
    public void PopulateUI(List<GameObject> zombies)
    {
        int numOfSlots = zombies.Count;
        if (numOfSlots > 0)
        {
            MenuObject.SetActive(true);
            for (int i = 0; i < numOfSlots; i++)
            {
                GameObject tempSlot = Instantiate(slotPrefab, UIParent.transform);
                slots.Add(tempSlot.GetComponent<ZombieSlot>());
                slots[i].AddZombie(zombies[i]);
            }
        }
    }
    public void SetPlayerCurseInventory(CurseInventory curseInventory)
    {
        playerCurseInv = curseInventory;
    }
    public void Update()
    {
        if (slots.Count>0)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].slotPressed)
                {
                    EventManager.instance.HealPlayerTrigger(slots[i].currentHealth);
                    ChooseZombie(slots[i].curse);
                }
            }
        }
    }
    public void ChooseZombie(Curse curse)
    {
        Curse chosenCurse = curse;
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].RemoveZombie();
        }
        slots.Clear();

        //Add curse to player curse inventory
        playerCurseInv.UnlockCurse(curse);
        MenuObject.SetActive(false);
        GameState.instance.UnPauseGame();
    }
}
