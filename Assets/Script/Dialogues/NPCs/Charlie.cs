using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charlie : NPC
{
    // Start is called before the first frame update
    public GameObject ally;
    public override void notifyDialogueOutcome(int outcome){
        if(outcome == 0){
            Debug.Log("Outcome " + outcome + " is achieved");
            finishedConversation = true;
            ally.SetActive(true);
        } else{
            finishedConversation = false;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
