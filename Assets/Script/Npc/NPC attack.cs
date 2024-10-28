using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCattack : MonoBehaviour
{
    private bool readyToShoot = true;
    private Animator animator;
    public int collisionCount = 0;
    private bool isInShootingRange = false;
    
    void Start()
    {
        animator = GetComponentInParent<Animator>();

    }

    void Update(){
        if(IsNotTriggering){
            isInShootingRange = false;
            animator.SetBool("isInShootingRange",isInShootingRange);
        } else{
            isInShootingRange = true;
            animator.SetBool("isInShootingRange",isInShootingRange);

        }
    }
    public bool IsNotTriggering
    {
        get {return collisionCount == 0;}
    }


    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Enemy"){
            collisionCount++;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag == "Enemy"){
            collisionCount--;
        }
    }


}
