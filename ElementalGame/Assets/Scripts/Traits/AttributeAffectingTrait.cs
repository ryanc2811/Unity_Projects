using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Trait", menuName = "Traits/AttributeAffectingTrait")]
public class AttributeAffectingTrait : Trait
{
    public Attribute alteredAttribute;
    public float value;
}
