using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Globals Instance;

    public LayerMask whatIsGround;
    public Transform feetPos;

    private void Start()
    {
        Instance = this;
    }

    public bool IsInLayerMask(int layer, LayerMask lm)
    {
        return lm == (lm | (1 << layer));
    }
}
