using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTest : MonoBehaviour
{
    public Spell spell;
    public GameObject player;
    void Start()
    {
        spell.Initialize(player);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spell.TriggerAbility(Input.mousePosition);
        }
    }
}
