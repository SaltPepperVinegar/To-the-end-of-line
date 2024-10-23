using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{   
    private Transform _playerTransform;
    private float _MovementSpeed  {get;set;}= 3f ;
    public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
        public override void EnterState() 
        {
            //Debug.Log("chase!");
        }


        public override void ExitState()
        {

        }

        public override void FrameUpdate(){
            enemy.Move(Vector2.zero);
            if (!enemy.IsAggroed)
            {
            enemyStateMachine.ChangeState(enemy.IdleState);
            }
            if (enemy.IsWthinStrikingDistance)
            {
            enemyStateMachine.ChangeState(enemy.AttackState);
            }

            Vector2 moveDirection = (_playerTransform.position - enemy.transform.position).normalized;
            
            enemy.Move(moveDirection*_MovementSpeed);

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            enemy.transform.rotation = Quaternion.Euler(0, 0, angle);

        }

        public override void PhysicsUpdate(){}

        public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType){}

}
