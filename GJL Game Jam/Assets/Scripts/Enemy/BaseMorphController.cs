using GameEngine.BehaviourManagement;
using GameEngine.BehaviourManagement.StateMachine_Stuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMorphController : MorphController
{
    protected override void InitialiseStateMachine()
    {
        IStateFactory stateFactory = new StateFactory();
        //Create State machines
        stateMachine = new EnemyStateMachine();
        stateMachine.AddState((int)BaseEnemyStates.FindTarget, stateFactory.Create<BaseEnemyFindTargetState>());
        stateMachine.AddState((int)BaseEnemyStates.Attack, stateFactory.Create<BaseEnemyAttackState>());

        ((IAIComponent)stateMachine).SetAIUser(this);
        ((IAIComponent)stateMachine).Initialise();

        receiveDamage = GetComponent<CurrentHealth>();
        receiveDamage.OnDamageTaken += DamageTaken;
    }

    protected void DamageTaken(MorphType damageType, float damage)
    {
        MorphSpawner.Instance.Spawn(damageType, transform.position);
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        receiveDamage.OnDamageTaken -= DamageTaken;
    }
}
