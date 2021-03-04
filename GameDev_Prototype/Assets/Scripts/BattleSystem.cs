using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public BattleState state;

    private Unit playerUnit;
    private Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;


    bool enemyAttacking = false;
    private bool blocked;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;

        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO=Instantiate(playerPrefab, playerBattleStation);
        playerUnit=playerGO.GetComponent<Unit>();
        GameObject enemyGO=Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        yield return new WaitForSeconds(2f);
        EnemyTurn();
    }

    private void PlayerTurn()
    {
        state = BattleState.PLAYERTURN;
        Debug.Log("Player turn");
    }

    private void EnemyTurn()
    {
        //Start enemy turn
        state = BattleState.ENEMYTURN;
        Debug.Log("Enemy Turn");
        StartCoroutine(EnemyAttack());
    }

    IEnumerator Block()
    {
        Debug.Log("Blocked Attack");
        blocked = true;
        yield return new WaitForSeconds(2f);
        EndPlayerTurn();
    }
    IEnumerator MissBlock()
    {
        bool isDead = playerUnit.CheckIsDead(enemyUnit.damage);
        playerHUD.SetHP(playerUnit.GetCurrentHealth());
        Debug.Log("Missed Block");
        yield return new WaitForSeconds(2f);
        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
            EnemyTurn();
    }

    private void EndBattle()
    {
        if (state == BattleState.LOST)
            Debug.Log("Battle Lost");
        else
            Debug.Log("Battle Won");
    }

    private void EndPlayerTurn()
    {
        if (!blocked)
            BlockMiss();
        else
            EnemyTurn();
        blocked = false;
    }

    IEnumerator EnemyAttack()
    {
        Debug.Log("Enemy Attack");
        PlayerTurn();
        enemyAttacking = true;
        yield return new WaitForSeconds(2f);
        enemyAttacking = false;
        EndPlayerTurn();
    }
    public void BlockAttack()
    {
        if (state == BattleState.PLAYERTURN)
        {
            if (enemyAttacking)
                BlockSuccess();
        }
    }
    private void BlockSuccess()
    {
        if(state==BattleState.PLAYERTURN)
            StartCoroutine(Block());
    }
    private void BlockMiss()
    {
        if (state == BattleState.PLAYERTURN)
            StartCoroutine(MissBlock());
    }
}
