using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrefabSpell : MonoBehaviour
{
    protected ElementType spellElement;
    protected int damage;
    [SerializeField]
    protected float tickDelay=0.5f;
    public void SetDamage(int d)
    {
        damage = d;
    }
    public void SetElementType(ElementType et)
    {
        spellElement = et;
    }
}
