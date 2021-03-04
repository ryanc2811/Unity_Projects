using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Equippable", menuName = "Game/Inventory/Item/Pickupable")]
public class Pickupable : Item
{
    public bool dropable = true;
    void Start()
    {
       
    }
   /// <summary>
   /// USES THE PICKUPABLE OBJECT
   /// </summary>
    public override void Use()
    {
        base.Use();
    }
}
