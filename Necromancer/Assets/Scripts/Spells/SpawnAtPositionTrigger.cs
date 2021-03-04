using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtPositionTrigger : MonoBehaviour
{
    public int damage;
    public GameObject effect;
    public float duration;
    public GameObject prefab;
    public void Initialize()
    {

    }
    public void CastSpell(Vector3 position)
    {
        StartCoroutine(SpellEffect(position));
    }
    private IEnumerator SpellEffect(Vector3 position)
    {
        GameObject tempSpell=Instantiate(prefab, position, Quaternion.identity, null);
        //GameObject tempEffect=Instantiate(effect, position, Quaternion.identity, null);
        PrefabSpell prefabSpell = tempSpell.GetComponent<PrefabSpell>();
        if (prefabSpell)
        {
            prefabSpell.SetDamage(damage);
        }
        //Wait for .07 seconds
        yield return new WaitForSeconds(duration);

        //Deactivate our line renderer after waiting
        Destroy(tempSpell);
       // Destroy(tempEffect);
    }
}
