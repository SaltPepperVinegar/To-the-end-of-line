using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{          
    public DialogueSO[] conversations;
    private Transform player;
    private SpriteRenderer speechBubbleRenderer;

    private DialogueManager dialogueManager;
    private bool dialogueInitiated;
    public GameObject npcFigure;
    private NPC nPC;

    private bool inRange = false;
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        nPC = GetComponentInParent<NPC>();
        speechBubbleRenderer = GetComponent<SpriteRenderer>();
        speechBubbleRenderer.enabled = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" )
        {
            inRange = true;
            /*
            player = collision.gameObject.GetComponent<Transform>();
            Vector2 direction = player.position - npcFigure.transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            npcFigure.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
            */
            if (! nPC.finishedConversation){
                speechBubbleRenderer.enabled = true;
                dialogueManager.initiateDialogue(this,nPC);
            }
        }
    }
    public void dialogueTurnedOff(){
        if(nPC.finishedConversation){
            speechBubbleRenderer.enabled = false;
        } else if (inRange){
            Debug.Log("reinitiate conversation");
            dialogueManager.initiateDialogue(this,nPC);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" )
        {
            inRange = false;

            speechBubbleRenderer.enabled = false;
            Debug.Log("conversation out of range");
            dialogueManager.TurnOffDialogue();
        }
    }

}
