using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadout : MonoBehaviour
{

    private List<Equippable> loadout;

    public int maxWeapons=2;

    private int currentWeaponIndex=0;

    // Start is called before the first frame update
    void Start()
    {
        loadout = new List<Equippable>();
        SelectWeapon();
    }

    public void AddWeapon(Equippable weapon)
    {
        if (loadout.Capacity >= maxWeapons)
            return;

        loadout.Add(weapon);
    }
    
    // Update is called once per frame
    void Update()
    {
        int previousWeaponIndex = currentWeaponIndex;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (currentWeaponIndex >= maxWeapons-1)
            {
                currentWeaponIndex = 0;
                Debug.Log(currentWeaponIndex);
            }
            else
            {
                Debug.Log(currentWeaponIndex);
                currentWeaponIndex++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (currentWeaponIndex <= 0)
            {
                currentWeaponIndex = maxWeapons-1;
                Debug.Log(currentWeaponIndex);
            }
            else
            {
                Debug.Log(currentWeaponIndex);
                currentWeaponIndex--;
            }
        }

        if (currentWeaponIndex != previousWeaponIndex)
        {
            //SELECT NEXT WEAPON
            SelectWeapon();
        }

    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach(Equippable weapon in loadout)
        {
            if (i == currentWeaponIndex)
            {
                //EQUIP WEAPON
                EquipWeapon(weapon);
            }
            else
            {
                //UNEQUIP WEAPON
                UnEquipWeapon(weapon);
            }
            i++;
        }
    }
    public void EquipWeapon(Equippable weapon)
    {
        if (loadout.Contains(weapon))
        {
            weapon.transform.gameObject.SetActive(true);
        }
    }
    public void UnEquipWeapon(Equippable weapon)
    {
        if (loadout.Contains(weapon))
        {
            weapon.transform.gameObject.SetActive(false);
        }
    }
}
