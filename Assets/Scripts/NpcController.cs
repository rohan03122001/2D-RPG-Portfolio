using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour, Interactable
{
    [SerializeField] Dialogues dialog;
    [SerializeField] List<Vector2> movementPattern;
    [SerializeField] float timeBetweenPatterns;

    Character character;
    NPCState State;


    float idleTimer = 0;
    int currentPattern = 0;

    private void Awake()
    {
        character = GetComponent<Character>();
    }
    public void Interact(Transform initiator)
    {
        State = NPCState.Dialog;
        character.LookTowards(initiator.position);
        StartCoroutine(DialogueManager.instance.ShowDialogue(dialog, () =>
        {
            idleTimer = 0f;
            State = NPCState.Idle;
        }));

    }

    private void Update()
    {
        if (State == NPCState.Idle)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer > timeBetweenPatterns)
            {
                idleTimer = 0f;
                if(movementPattern.Count >0)
                    StartCoroutine(Walk());
            }
        }
        character.HandleUpdate();
    }

    IEnumerator Walk()
    {
        State = NPCState.Walking;

        var oldPos = transform.position;

        yield return character.Move(movementPattern[currentPattern]);

        //if(transform.position != oldPos)
            currentPattern = (currentPattern + 1) % movementPattern.Count;

        State = NPCState.Idle;
    }
    
}

public enum NPCState {Idle, Walking, Dialog}