using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    public new string name;
    public Sprite image;
    public float cooldown;
    public AudioClip sound;
    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility(Vector3 position);
}
