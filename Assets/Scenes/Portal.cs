using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting;

public class Portal : MonoBehaviour, iPlayerTriggerable
{
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationIdentifier DestinationPortal;
    [SerializeField] AudioClip Music;

    PlayerController Player;
   public void onPlayerTriggered(PlayerController player)
    {
        this.Player = player;
        StartCoroutine(SwitchScene());
    }

    Fader fader;

    private void Start()
    {
        fader = FindObjectOfType<Fader>();
    }

    IEnumerator SwitchScene()
    {
        DontDestroyOnLoad(gameObject);

        GameController.instance.PauseGame(true);

        yield return fader.FadeIn(0.5f);

        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        if(Music != null)
            AudioManager.instance.PlayMusic(Music);

        var destPortal = FindObjectsOfType<Portal>().First(x => x != this && x.DestinationPortal == this.DestinationPortal);
        //Player.transform.position = destPortal.transform.position;
        Player.Character.setPositionSnapToTile(destPortal.spawnPoint.position);
        yield return fader.FadeOut(0.5f);
        GameController.instance.PauseGame(false);

        Destroy(gameObject);
    }

    public Transform SpawnPoint => spawnPoint;
}


public enum DestinationIdentifier { A, B, C, D }