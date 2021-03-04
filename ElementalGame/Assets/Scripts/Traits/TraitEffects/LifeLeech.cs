using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeLeech : MonoBehaviour,ITraitEffect
{
    float percentHealthTaken=0.1f;
    public void StartEffect(GameObject enemy)
    {
        GameObject player = PlayerController.instance.Mover;
        Debug.Log(player);
        Stats enemyStats = enemy.GetComponent<Stats>();
        float healthToBeAdded = enemyStats.GetValue(AttributeType.MaxHealth) * percentHealthTaken;
        player.GetComponent<CurrentHealth>().Heal((int)healthToBeAdded);
    }
}
