using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsUI : MonoBehaviour
{
    public GameObject ammoSlotPrefab;

    public Transform ammoGrid;

    private List<AmmoSlot> ammoSlots=new List<AmmoSlot>();
    public int ammoCount;

    public void AmmoSpent()
    {
        ammoCount--;
        ammoSlots[ammoCount].AmmoUsed();
    }
    public void NewAmmoCountSet(int newAmmoCount)
    {
        if (ammoSlots.Capacity > 0)
        {
            for (int i = 0; i < ammoSlots.Count; i++)
            {
                ammoSlots[i].Remove();
            }
        }
        ammoSlots.Clear();
        ammoCount = newAmmoCount;
        for (int i = 0; i < ammoCount; i++)
        {
            GameObject ammoSlotObject= Instantiate(ammoSlotPrefab, ammoGrid);
            AmmoSlot ammoSlot = ammoSlotObject.GetComponent<AmmoSlot>();
            ammoSlot.SetActive();
            ammoSlots.Add(ammoSlot);
        }
    }
}
