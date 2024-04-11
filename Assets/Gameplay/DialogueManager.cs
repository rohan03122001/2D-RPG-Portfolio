using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] public GameObject dialogueBox;
    [SerializeField] public TMPro.TextMeshProUGUI dialogueText;
    [SerializeField] public int lettersPerSecond;
    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    public static DialogueManager instance { get; private set; }


    private void Awake()
    {
        instance = this;
    }

    int currLine = 0;
    Dialogues dialog;
    bool isTyping;
    Action onDialogFinished;
    public bool IsShowing { get; private set; }
    public IEnumerator ShowDialogue(Dialogues dialog, Action onFinished=null)
    {
       
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();

        IsShowing = true;
        this.dialog = dialog;
        onDialogFinished = onFinished;

        dialogueBox.SetActive(true);
        
        StartCoroutine(TypeDialogue(dialog.Lines[0]));
    }
    public void HandleUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) && !isTyping)
        {
            ++currLine;
            if(currLine< dialog.Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialog.Lines[currLine]));
            }
            else
            {
                currLine = 0;
                IsShowing = false;
                dialogueBox.SetActive(false);
                onDialogFinished?.Invoke();
                OnCloseDialog?.Invoke();

            }
        }
    }
        
    public IEnumerator TypeDialogue(string dialog)
    {

        isTyping = true;
        dialogueText.text = ""; 
        foreach(var letter in dialog.ToCharArray())
        {
            dialogueText.text += letter; ;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }   
}
