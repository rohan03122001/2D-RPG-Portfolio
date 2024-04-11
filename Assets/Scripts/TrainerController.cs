using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerController : MonoBehaviour
{
    [SerializeField] GameObject image;
    [SerializeField] Dialogues dialog;

    Character character;

    public static TrainerController instance;


    private void Awake()
    {
        instance = this;
        character = GetComponent<Character>();
    }
    public IEnumerator TriggerNPC(PlayerController player)
    {
        yield return new WaitForSeconds(0.5f);
        image.SetActive(true);
        yield return new WaitForSeconds(2f);
        image.SetActive(false);

        var diff = player.transform.position - transform.position;
        var moveVec = diff - diff.normalized;

        moveVec = new Vector2(Mathf.Round(moveVec.x), Mathf.Round(moveVec.y));

        yield return character.Move(moveVec);

        StartCoroutine(DialogueManager.instance.ShowDialogue(dialog));
        
    }


}
