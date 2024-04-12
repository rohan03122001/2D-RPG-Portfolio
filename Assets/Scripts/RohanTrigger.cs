using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RohanTrigger : MonoBehaviour
{
    [SerializeField] GameObject image;
    [SerializeField] Dialogues dialog;
    [SerializeField] AudioClip audioClip;
    //Character character;

    public static RohanTrigger instance;


    private void Awake()
    {
        instance = this;
        //character = GetComponent<Character>();
    }
    public IEnumerator TriggerNPC()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlaySfx(audioClip);
        image.SetActive(true);
        yield return new WaitForSeconds(2f);
        image.SetActive(false);

        StartCoroutine(DialogueManager.instance.ShowDialogue(dialog));
    }

}
