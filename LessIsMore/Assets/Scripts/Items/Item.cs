using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="Game/Inventory/Item")]
public class Item : ScriptableObject
{
    public new string name="Item";
    public Sprite icon=null;
    public Transform transform;
    public virtual void Use()
    {

    }
}
