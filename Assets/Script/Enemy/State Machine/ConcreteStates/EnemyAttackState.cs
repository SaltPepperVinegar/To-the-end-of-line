using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{

    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }
    public override void EnterState() 
    {
        //Debug.Log("attack!");

    }


    public override void ExitState() {    }

    public override void FrameUpdate(){
        enemy.Move(Vector2.zero);
        if (enemy.attackReady){
            enemy.StartAttack();
        }


        if (!enemy.IsWthinStrikingDistance)
        {
            enemyStateMachine.ChangeState(enemy.ChaseState);
        }

    }

    public override void PhysicsUpdate(){}

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType){}

}
