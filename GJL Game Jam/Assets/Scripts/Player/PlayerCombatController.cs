using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    private IList<IWeapon> weapons;
    public Transform weaponHolder;
    private int currentWeaponIndex = -1;
    private PlayerPlatformerController platformerController;
    void Awake()
    {
        platformerController = GetComponent<PlayerPlatformerController>();
        RefreshLoadout();
    }

    void RefreshLoadout()
    {
        weapons = new List<IWeapon>();
        for (int i = 0; i < weaponHolder.childCount; i++)
        {
            if(weaponHolder.GetChild(i).GetComponent<IWeapon>()!=null)
                weapons.Add(weaponHolder.GetChild(i).GetComponent<IWeapon>());
        }
        if (weapons.Count >= 0)
            currentWeaponIndex = 0;
        weapons[currentWeaponIndex].IsActive = true;
        GetComponent<Animator>().SetTrigger(weapons[currentWeaponIndex].AnimationKey);
        Debug.Log(weapons.Count);
    }
    void FinishAttack()
    {
        weapons[currentWeaponIndex].FinishAttack();
    }
    void CheckAttackHitBox()
    {
        weapons[currentWeaponIndex].CheckAttackHitBox();
    }
    void SwitchHands()
    {
        if (platformerController.moveDirection == 1)
        {
            weaponHolder.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            weaponHolder.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        SwitchHands();
        if(weapons.Count>1)
            SwitchWeapons();
    }
    void ChangeActiveWeapon(int newWeaponIndex)
    {
        weapons[currentWeaponIndex].IsActive = false;
        weapons[newWeaponIndex].IsActive = true;
        currentWeaponIndex = newWeaponIndex;
        GetComponent<Animator>().SetTrigger(weapons[currentWeaponIndex].AnimationKey);
    }
    void SwitchWeapons()
    {
        int nextWeaponIndex = 0;
        if (Input.GetButtonDown("PreviousWeapon"))
        {
            if (currentWeaponIndex == 0)
            {
                nextWeaponIndex = weapons.Count-1;
            }
            else
            {
                nextWeaponIndex = currentWeaponIndex - 1;
            }
            Debug.Log(nextWeaponIndex);
            ChangeActiveWeapon(nextWeaponIndex);
        }
        else if (Input.GetButtonDown("NextWeapon"))
        {
            if (currentWeaponIndex == weapons.Count-1)
            {
                nextWeaponIndex = 0;
            }
            else
            {
                nextWeaponIndex = currentWeaponIndex + 1;
            }

            ChangeActiveWeapon(nextWeaponIndex);
        }
    }
    
}
