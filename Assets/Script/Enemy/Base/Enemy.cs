using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour , IEnemyMoveable,ITriggerCheckable
{
    public Rigidbody2D RB { get; set;}   

    public EnemyStateMachine StateMachine {get;set;}
    public EnemyState IdleState {get;set;}
    public EnemyState ChaseState {get;set;}
    public EnemyState AttackState {get;set;}
    public bool IsAggroed { get; set;}
    public bool IsWthinStrikingDistance { get; set;}

    public float RandomMovementRange = 5f;
    public float RandomMovementSpeed = 1f;

    public bool attackReady;
    public bool attacking;
    public float attackTimer = 2f; 

    public bool isMoving;

    private Animator animator;
    private void Awake(){
        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this,StateMachine);
        ChaseState = new EnemyChaseState(this,StateMachine);
        AttackState = new EnemyAttackState(this,StateMachine);

        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start(){
        StateMachine.Initialize(IdleState);
        attackReady =true;
        attacking = false;
    }
    

    public void Update(){

        StateMachine.CurrentEnemyState.FrameUpdate();
        if (RB.velocity == Vector2.zero){
            isMoving = false;
        } else{
            isMoving = true;
        }
        animator.SetBool("IsMoving",isMoving);
    }

    public void FixedUpdate(){
        StateMachine.CurrentEnemyState.PhysicsUpdate();

    }


    public void Move(Vector2 velocity)
    {
        RB.velocity = velocity; 
    }

    private void AnimationTriggerEvent (AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetStrikingDistanceBool(bool isWthinStrikingDistance)
    {
        IsWthinStrikingDistance = isWthinStrikingDistance;
    }

    public enum AnimationTriggerType{

    }
    public virtual void StartAttack(){
        animator.SetTrigger("Attack");
        attackReady = false;
        StartCoroutine(rechargeAttack());
    }

    IEnumerator rechargeAttack(){
        yield return new WaitForSeconds(attackTimer);
        attackReady = true;
    }
    public void Attack(){
        if(attacking ){
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Damage(1);
        }        
    }

    
}
