using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICurseEffect
{
    void ApplyCurse();
    void RemoveCurse();
    void Initialize(GameObject player);
}
