using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private DialogueSO currentConversation;
    private int stepNum;

    public bool dialogueActivated;

    private GameObject dialogueCanvas;
    private TMP_Text actor;
    private Image portrait;
    private TMP_Text dialogueText;

    private string currentSpeaker;
    private Sprite currentPortrait;
    
    public ActorSo[] actorSO;

    public GameObject[] optionButton;
    private TMP_Text[] optionButtonText;
    private GameObject optionsPanels;

    [SerializeField]
    private float typingSpeed = 0.02f;
    private Coroutine typeWriterRoute;
    private bool canContinueText =true;
    private bool isSkipping = false;
    // Start is called before the first frame update
    private NPC currentNPC;
    private NPCDialogue currentNPCDialogue;
    void Start()
    {   
        dialogueCanvas = GameObject.Find("DialogueCanvas");


        optionsPanels = GameObject.Find("OptionsPanel");
        optionsPanels.SetActive(false);

        optionButtonText = new TMP_Text[optionButton.Length];
        for(int i = 0; i< optionButton.Length; i++)
        {
            optionButtonText[i] = optionButton[i].GetComponentInChildren<TMP_Text>();
            optionButton[i].SetActive(false);
        }


        actor = GameObject.Find("ActorText").GetComponent<TMP_Text>();
        portrait = GameObject.Find("Portrait").GetComponent<Image>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<TMP_Text>();

        dialogueCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueActivated && Input.GetButtonDown("Interact"))
        {   
            if (canContinueText)
            {
                if(stepNum >= currentConversation.actors.Length)
                {
                    Debug.Log("out of range convesation so off");
                    TurnOffDialogue();
                }
                else 
                {
                    PlayDialogue();
                }
            } else{
                isSkipping = true;
            }
        }
    }
    void PlayDialogue()
    {   
        if(currentConversation.actors[stepNum] == DialogueActors.Random)
            SetActorInfo(false);
        else
            SetActorInfo(true);


        actor.text = currentSpeaker;
        portrait.sprite = currentPortrait;

        if(currentConversation.actors[stepNum] == DialogueActors.Branch)
        {
            for (int i =0; i < currentConversation.optionText.Length; i++)
            {
                if (currentConversation.optionText[i] == null)
                    optionButton[i].SetActive(false);
                else{
                    optionButtonText[i].text = currentConversation.optionText[i];
                    optionButton[i].SetActive(true);
                }

                //set the first button to be tauto selected 
                optionButton[0].GetComponent<Button>().Select();
            }
        }
        if(stepNum <currentConversation.Dialogue.Length)
        {
            //make sure routine only run once
            if(typeWriterRoute != null)
                StopCoroutine(typeWriterRoute);
            typeWriterRoute = StartCoroutine(TypeWriterEffect(dialogueText.text = currentConversation.Dialogue[stepNum]));
        }
        else 
            optionsPanels.SetActive(true);
        dialogueCanvas.SetActive(true);
        //Debug.Log(currentConversation.Dialogue[stepNum]);
        if(currentConversation.actors[stepNum] != DialogueActors.Branch)
        {
            stepNum += 1;
        }
            


    }
    void SetActorInfo(bool recurringCharacter)
    {
        if (recurringCharacter)
        {
            for (int i = 0 ; i < actorSO.Length; i++)
            {
                 if(actorSO[i].name == currentConversation.actors[stepNum].ToString())
                 {
                    currentSpeaker = actorSO[i].actorName;
                    currentPortrait = actorSO[i].actorPortrait;
                    Debug.Log("current portrait is set to " + actorSO[i].actorPortrait);

                 }
            }
        } else{
            currentSpeaker = currentConversation.randomActorName;
            currentPortrait = currentConversation.randomActorPortrait;
        }
    }
    public void Option(int optionNum){
            Debug.Log("choice made"+ optionNum);
        foreach(GameObject button in optionButton )
            button.SetActive(false);
        switch (optionNum){
            case 0:
                currentConversation = currentConversation.option0;
                currentNPC.notifyDialogueOutcome(0);
                break;
            case 1:
                currentConversation = currentConversation.option1;
                currentNPC.notifyDialogueOutcome(1);

                break;
            case 2:
                currentConversation = currentConversation.option2;
                currentNPC.notifyDialogueOutcome(2);

                break;
            case 3:
                currentConversation = currentConversation.option3;
                currentNPC.notifyDialogueOutcome(3);

                break;
        
        }
        stepNum = 0;
        PlayDialogue();
    }

    private IEnumerator TypeWriterEffect(string line)
    {
        dialogueText.text = "";
        canContinueText = false;
        yield return new WaitForSeconds(.5f);
        foreach (char letter in line.ToCharArray())
        {   
            if(isSkipping)
            {   
                Debug.Log("skip!");
                dialogueText.text = line;
                isSkipping = false;
                break;

            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    canContinueText = true;

    }
    public void initiateDialogue(NPCDialogue npcDialogue, NPC nPC)
    {      
        currentNPC = nPC;
        currentNPCDialogue = npcDialogue;
        currentConversation = npcDialogue.conversations[0];
        Debug.Log("dialog turned on");

        dialogueActivated = true;
    }

    public void TurnOffDialogue()
    {   
        //Debug.Log("dialog turned off");

        stepNum = 0;
        dialogueActivated = false;
        optionsPanels.SetActive(false);
        dialogueCanvas.SetActive(false);
        currentNPC = null;
        if(currentNPCDialogue != null){
            currentNPCDialogue.dialogueTurnedOff();
        }
        currentNPCDialogue = null;


    }
}


public enum DialogueActors
{
    Guard,
    Charlie,
    Tom,
    Nick,
    Survivor,
    Random,
    Branch,
};