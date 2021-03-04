using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour, IInteractable
{
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        item.transform = transform;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// PICKS UP THE PICKUPABLE ITEM AND ADDS IT TO INVENTORY
    /// </summary>
    public void doInteraction()
    {
        //if (item is Equippable)
        //{
        //    if (PlayerLoadout.Instance.Loadout[(int)EquipmentSlot.PickupGun] == null)
        //    {
        //        PlayerLoadout.Instance.EquipItem((Equippable)item);
        //        PlayerLoadout.Instance.UnequipItem((int)EquipmentSlot.StarterGun);
        //        transform.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        transform.gameObject.SetActive(false);
        //    }
        //}
    }

    #region getter for instance
    /// <summary>
    /// getter propery for returning an instance of this object
    /// </summary>
    /// <returns></returns>
    public IInteractable Instance()
    {
        return this;
    }
    #endregion
}
