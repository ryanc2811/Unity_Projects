using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Spell", menuName = "Spells/SpawnAtPositionSpell")]
public class SpawnAtPositionSpell : Spell
{
    public int damage;
    private SpawnAtPositionTrigger trigger;
    public GameObject effect;
    public GameObject prefab;
    public float duration;
    public override void Initialize(GameObject obj)
    {
        trigger = obj.GetComponent<SpawnAtPositionTrigger>();
        trigger.Initialize();
        trigger.damage = damage;
        trigger.effect = effect;
        trigger.prefab = prefab;
        trigger.duration = duration;
        trigger.spellElement = element;
    }

    public override void TriggerAbility(Vector3 position)
    {
        trigger.CastSpell(position);
    }
}
