using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brutus : MonoBehaviour
{

    public enum State
    {
        Happy,
        Sad,
        Raging,
        Feeding
    }

    [SerializeField] private HappinessUI happinessUI;

    [SerializeField] private int maxHappiness=100;
    [SerializeField] private int happiness=70;

    public float rageDuration = 5f;

    public State currentState;


    [SerializeField] Transform player;
    [SerializeField] private AIPath ai;

    bool attackOnCooldown = false;

    void Start()
    {
        currentState = State.Happy;
        happinessUI.UpdateUI(happiness,maxHappiness);
    }

    public void LoseHappiness()
    {
        int happinessToLose = 10;

        happiness -= happinessToLose;
        if (happiness < 0)
        {
            happiness = 0;
        }

        happinessUI.UpdateUI(happiness, maxHappiness);
    }

    public void GainHappiness()
    {
        int happinessToGain = 10;
        happiness += happinessToGain;
        if (happiness > maxHappiness)
        {
            happiness = maxHappiness;
        }

        happinessUI.UpdateUI(happiness, maxHappiness);
    }

    private float ConvertHappinessToPercent()
    {
        return (float)happiness/maxHappiness;
    }
    private void Update()
    {
        switch (currentState)
        {
            case State.Happy:
                if (ConvertHappinessToPercent() < 0.4f&& ConvertHappinessToPercent() > 0f)
                    currentState = State.Sad;
                if (ConvertHappinessToPercent() == 0)
                {
                    currentState = State.Raging;
                    StartCoroutine(RageTimer());
                }

                Happy();
                break;
            case State.Sad:
                if (ConvertHappinessToPercent() > 0.4f)
                    currentState = State.Happy;
                if (ConvertHappinessToPercent() == 0)
                {
                    currentState = State.Raging;
                    StartCoroutine(RageTimer());
                }
                Sad();
                break;
            case State.Raging:
                Raging();
                break;
        }
        //Debug.Log(currentState);
    }

    private IEnumerator RageTimer()
    {
        Debug.Log("raging");
        
        yield return new WaitForSeconds(rageDuration);
        Debug.Log("raged");
        FinishRage();
    }

    private void FinishRage()
    {
        StopCoroutine(RageTimer());
        if (currentState == State.Raging)
        {
            GainHappiness();
            currentState = State.Sad;
        }
       
    }
    private void Raging()
    {
        ai.destination = player.position;
        if (Vector3.Distance(transform.position,player.position)<1f&&!attackOnCooldown)
        {
            PlayerStats.Instance.TakeDamage(0.5f);
            StartCoroutine(AttackCooldown());
            FinishRage();
        }
    }
    private void Sad()
    {
        
    }

    private IEnumerator AttackCooldown()
    {
        attackOnCooldown = true;
        yield return new WaitForSeconds(5f);
        attackOnCooldown = false;
    }

    private void Happy()
    {
        
    }
}
