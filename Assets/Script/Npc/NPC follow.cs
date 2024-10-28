using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCfollow : MonoBehaviour
{
    private AllyNpc NPC;
    private GameObject player; 
    public float MovementSpeed = 10f;
    private Animator animator;
    private bool isFollowing =true;
    private bool isCoroutineRunning;
    void Start(){
        NPC = GetComponentInParent<AllyNpc>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponentInParent<Animator>();
    }
    void Update(){
        if(isFollowing && !animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot")){

            Vector2 moveDirection = (player.transform.position - transform.position).normalized;
            NPC.Move(moveDirection*MovementSpeed);

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            NPC.transform.rotation = Quaternion.Euler(0, 0, angle);

        } else{
            NPC.Move(Vector2.zero);
        }
        animator.SetBool("IsMoving",isFollowing);


    }
    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject == player)
        {   
            isFollowing = false;
        }
    } 
    void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject == player)
        {   if (!isCoroutineRunning){
                StartCoroutine(StartFollowing());            

            }
        }
    } 
    private IEnumerator StartFollowing(){
        isCoroutineRunning = true;

        yield return new WaitForSeconds(0.5f);
        isFollowing = true;
        isCoroutineRunning = false;

    }

}
