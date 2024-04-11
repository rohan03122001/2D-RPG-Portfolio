using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour, Interactable
{
    public AudioClip Bootup;
    public GameObject screen;
    
    public static Computer instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    public void Interact(Transform initiator)
    {
        GameController.instance.interactingWithComputer = true;
        
        AudioManager.instance.PlaySfx(Bootup);
        screen.SetActive(true);
    }
}
