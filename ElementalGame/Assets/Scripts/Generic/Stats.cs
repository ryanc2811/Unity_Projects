using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public List<Attributes> attributes = new List<Attributes>();
    public EntityType entityType;
    public ElementType element;
    public float GetValue(AttributeType aType)
    {
        float value=0f;
        for (int i = 0; i < attributes.Count; i++)
        {
            if (attributes[i].type == aType)
            {
                value=attributes[i].value;
            }
        }
        return value;
    }
    public void SetValue(Attribute aType,float newValue)
    {
        for (int i = 0; i < attributes.Count; i++)
        {
            if (attributes[i].attribute == aType)
            {
                attributes[i].value+=newValue;
            }
        }
    }
}
public enum AttributeType
{
    MaxHealth,
    AttackDelay,
    AttackDamage,
    MoveSpeed
}
public enum EntityType
{
    Player,
    Zombie,
    Enemy
}
