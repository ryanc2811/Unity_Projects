using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICurseFactory
{
    ICurseEffect CreateCurse(CurseType curseType);
}
public enum CurseType
{
    Tether
}