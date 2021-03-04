using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Equippable",menuName = "Game/Inventory/Item/Equippable")]
public class Equippable : Pickupable
{
    public EquipmentSlot Slot;
    public int Damage = 0;
    
    /// <summary>
    /// EQUIPS THE EQUIPPABLE ITEM
    /// </summary>
    void Equip()
    {
        //Equip the item
        //PlayerLoadout.Instance.EquipItem(this);
        //remove the item from the inventory
        //PlayerInventory.Instance.RemoveItem(this);
    }
    /// <summary>
    /// DROPS THE EQUIPPABLE ITEM
    /// </summary>
    void Drop()
    {
        //if(dropable)
        //unequip the item from the loadout
            //PlayerLoadout.Instance.UnequipItem((int)Slot);
    }
}
#region Equipment slot
public enum EquipmentSlot
{
    StarterGun,
    PickupGun
}
#endregion
