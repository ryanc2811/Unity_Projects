using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Curse", menuName = "Curses/UniqueCurse")]
public class UniqueCurse : Curse
{
    public CurseType curseType;
    private ICurseEffect curseEffect;
    public void Initialize(GameObject player)
    {
        ICurseFactory curseFactory = new CurseFactory();
        curseEffect=curseFactory.CreateCurse(curseType);
        curseEffect.Initialize(player);

    }
    public void Curse()
    {
        curseEffect.ApplyCurse();
    }
    public void RemoveCurse()
    {
        curseEffect.RemoveCurse();
        curseEffect = null;
    }
}
