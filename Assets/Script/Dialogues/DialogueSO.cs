using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class DialogueSO : ScriptableObject
{
    public DialogueActors[] actors;

    [Tooltip("Only needed when the character is a random character")]
    [Header("Random Actor Info")]
    public string randomActorName;
    public Sprite randomActorPortrait;

    [Header("Dialogue")]
    [TextArea]
    public string[] Dialogue;

    [Tooltip("The words that will appear on option buttons")]
    public string[] optionText;

    public DialogueSO option0;
    public DialogueSO option1;
    public DialogueSO option2;
    public DialogueSO option3;

}
