using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nick : NPC
{
    // Start is called before the first frame update
    public override void notifyDialogueOutcome(int outcome){
        if(outcome == 0){
            Debug.Log("Outcome " + outcome + " is achieved");
            finishedConversation = true;
        } else{
            finishedConversation = false;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
