using GameEngine.BehaviourManagement;
using GameEngine.BehaviourManagement.StateMachine_Stuff;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MorphController : EnemyController, IPatrolAI, IMorphController
{
    [SerializeField]
    private Transform groundDetection;
    public Transform GroundDetection => groundDetection;
    [SerializeField]
    private Transform wallDetection;
    public Transform WallDetection => wallDetection;

    public MorphType morphType;

    private Action<MorphType, MorphType, float> MorphChangeEvent;
    public Action<MorphType,MorphType,float> MorphStateChangeRequest { get => MorphChangeEvent; set => MorphChangeEvent=value; }
    protected CurrentHealth receiveDamage;
    public CurrentHealth ReceiveDamage { get { return receiveDamage; } }

    protected override void InitialiseStateMachine()
    {
        
    }

}
