using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleEvent : MonoBehaviour
{
    

    public Brutus brutus;

    
    public void RunAway()
    {
        PlayerStats.Instance.TakeDamage(1);
        EndBattle();
    }
    public void Shoot()
    {
        PlayerStats.Instance.GiveMeat(3);
        PlayerStats.Instance.RemoveAmmo(1);
        EndBattle();
    }
    public void LetBrutusAttack()
    {
        brutus.LoseHappiness();
        EndBattle();
    }

    private void EndBattle()
    {
        PlayerStats.Instance.StopMoving = false;
        gameObject.SetActive(false);
    }
}
