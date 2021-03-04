using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attributes
{
    public Attribute attribute;
    public float value;
    public AttributeType type;
    public Attributes(Attribute attribute, float value,AttributeType type)
    {
        this.attribute = attribute;
        this.value = value;
        this.type = type;
    }
}
