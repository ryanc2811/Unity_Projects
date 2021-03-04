using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MorphObject", menuName = "ScriptableObjects/MorphObject")]
public class MorphObject:ScriptableObject
{
    public GameObject Prefab;
    public MorphType morphType;
}
