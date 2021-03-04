using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerUI : MonoBehaviour
{
    List<GameObject> slots = new List<GameObject>();

    public GameObject slotPrefab;
    public GameObject UIParent;
    public GameObject scrollView;
    public void PopulateUI(List<GameObject> zombies)
    {
        int numOfSlots = zombies.Count;
        if (numOfSlots > 0)
        {
            scrollView.SetActive(true);
            for (int i = 0; i < numOfSlots; i++)
            {
                GameObject tempSlot = Instantiate(slotPrefab, UIParent.transform);
                slots.Add(tempSlot);
                slots[i].GetComponent<ZombieSlot>().AddZombie(zombies[i]);
            }
        }
    }
    public void Update()
    {
        if (slots.Count>0)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].GetComponent<ZombieSlot>().slotPressed)
                {
                    ChooseZombie(slots[i].GetComponent<ZombieSlot>().zombie);
                }
            }
        }
    }
    public void ChooseZombie(GameObject zombie)
    {
        GameObject chosenZombie = zombie;
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<ZombieSlot>().RemoveZombie();
        }
        slots.Clear();
        scrollView.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("fafaf");
        EventManager.instance.NewPlayerTrigger(chosenZombie);
    }
}
