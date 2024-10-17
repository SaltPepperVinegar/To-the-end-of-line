using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Vector3 _targetPos;
    private Vector3 _direction;

    private bool isWalking;
    private float standingTimer;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }
    public override void EnterState() 
    {   isWalking = false;
        standingTimer = 0f;
        base.EnterState();
        _targetPos = GetRandomPointInCircle();
    }

    public override void ExitState()
    {

    }

    public override void FrameUpdate(){
            
        if (enemy.IsAggroed)
        {   
            
            enemyStateMachine.ChangeState(enemy.ChaseState);
        }

        if (isWalking == false){
            standingTimer += Time.deltaTime;
            enemy.Move(Vector2.zero);
            if(standingTimer>2f){
                standingTimer = 0f;
                isWalking = true;
            }

        } else{
            _direction = (_targetPos-enemy.transform.position).normalized;

            enemy.Move(_direction*enemy.RandomMovementSpeed);
            if ((enemy.transform.position-_targetPos).sqrMagnitude<0.01f){
                _targetPos = GetRandomPointInCircle();
                isWalking = false;
            }
                // Calculate the angle (in radians) and convert it to degrees
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            // Set the object's rotation to face the direction of movement (only rotating along the Z-axis)
            enemy.transform.rotation = Quaternion.Euler(0, 0, angle);

        }

    }

    public override void PhysicsUpdate(){}

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType){}


    private Vector3 GetRandomPointInCircle()
    {   
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle*enemy.RandomMovementRange;
    } 
}
