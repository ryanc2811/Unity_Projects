using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Trait",menuName ="Trait")]
public class Trait : ScriptableObject
{
    public float value;
    public new string name;
    public string description;
    public Sprite image;
    public Attribute alteredAttribute = Attribute.none;
    public bool unique = false;
}
