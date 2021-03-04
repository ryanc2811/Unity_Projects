using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseFactory : ICurseFactory
{
    public ICurseEffect CreateCurse(CurseType curseType)
    {
        switch (curseType)
        {
            case CurseType.Tether:
                return new TetherCurse();
            default: throw new InvalidOperationException("Invalid type specified.");
        }
    }
}
