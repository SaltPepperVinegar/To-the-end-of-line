using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    public bool finishedConversation = false;
    public virtual void notifyDialogueOutcome(int outcome){
        Debug.Log("Outcome " + outcome + " is achieved");
        finishedConversation = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
