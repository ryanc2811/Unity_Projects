using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Curse", menuName = "Curses/AttributeAffectingCurse")]
public class AttributeAffectingCurse : Curse
{
    public Attribute alteredAttribute;
    public float value;
}
