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

    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();

        speechBubbleRenderer = GetComponent<SpriteRenderer>();
        speechBubbleRenderer.enabled = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" )
        {
            speechBubbleRenderer.enabled = true;
            /*
            player = collision.gameObject.GetComponent<Transform>();
            Vector2 direction = player.position - npcFigure.transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            npcFigure.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
            */
            dialogueManager.initiateDialogue(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Player" )
        {

        speechBubbleRenderer.enabled = false;

        dialogueManager.TurnOffDialogue();
        }
    }

}
