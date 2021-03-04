using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    public int maxHealth = 10;
    public float attackDelay = 3f;
    public int attackDamage = 5;
    public float moveSpeed = 10f;
    public EntityType entityType;
    public bool activePlayer = false;
}
public enum Attribute
{
    maxHealth,
    attackDelay,
    attackDamage,
    moveSpeed,
    none
}
public enum EntityType
{
    Zombie,
    Enemy
}
