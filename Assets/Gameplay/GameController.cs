using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public enum GameState { FreeRoam, Dialog , Pause, Menu, cutscene}


public class GameController : MonoBehaviour
{
    public GameObject webview1;


    [SerializeField] PlayerController playerController;
    public bool interactingWithComputer = false;   
    
    public static GameController instance { get; private set; }

    MenuController menuController;
    private void Awake()
    {
        menuController = GetComponent<MenuController>();
        instance = this;
    }

    // Start is called before the first frame update
    public GameState state;
    GameState StateBeforePause;
    void Start()
    {
        state = GameState.FreeRoam;
        DialogueManager.instance.OnShowDialog += ()=>
        {
            state= GameState.Dialog;
        };
        DialogueManager.instance.OnCloseDialog += () =>
        {
            state = GameState.FreeRoam;
        };

        menuController.onBack += () =>
        {
            state = GameState.FreeRoam;
        };

        menuController.onMenuSelected += OnMenuSelected;
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            StateBeforePause = state;
            state = GameState.Pause;
        }
        else
        {
            state = StateBeforePause;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();

            //playerController.onEnterNPCview += (Collider2D trainerCollider) =>
            //{
            //    var trainer= trainerCollider.GetComponentInParent<TrainerController>();
            //    if(trainer != null)
            //    {
            //        StartCoroutine (trainer.TriggerNPC(playerController));
            //    }
            //};

            if ((Input.GetKeyDown(KeyCode.Z) || SimpleInput.GetButtonDown("Z")) && interactingWithComputer)
            {

                state = GameState.Menu;
                //AudioManager.instance.PlaySfx(Computer.instance.Bootup);
                StartCoroutine(Delay());
                
            }
        }
        else if(state == GameState.Dialog)
        {
            DialogueManager.instance.HandleUpdate();
        }
        else if(state == GameState.Menu)
        {
            menuController.HandleUpdate();
        }
    }

    void OnMenuSelected(int selectedItem)
    {
        if (selectedItem == 0)
        {
            menuController.CloseMenu();
            state = GameState.FreeRoam;
            GameController.instance.interactingWithComputer = false;
            StartCoroutine(RohanTrigger.instance.TriggerNPC());

            
            //webview1.OpenURL("https://rohanbhujbal.vercel.app/");
}
        else if(selectedItem == 1)
        {
            if (playerController.isMobile())
            {
                webview1.SetActive(true);

                // if (Input.GetKeyDown(KeyCode.X) || SimpleInput.GetButtonDown("X"))
                //webview1Script.CloseWebView();
            }
            else
            {
                Application.OpenURL("https://rohanbhujbal.vercel.app/");
            }
        }
        else if (selectedItem == 2)
        {

            if (playerController.isMobile())
            {
                SampleWebView.Instance.webViewObject.SetVisibility(true);
                webview1.SetActive(true);
                if (Input.GetKeyDown(KeyCode.X) || SimpleInput.GetButtonDown("X"))
                    webview1.SetActive(false);
            }
            else
            {

                Debug.Log("Third Option");
                // Application.ExternalEval("window.open(\"https://www.linkedin.com/in/rohan-bhujbal-031574213/\")");
                Application.OpenURL("https://www.linkedin.com/in/rohan-bhujbal-031574213/");
            }

        }
        else if (selectedItem == 3)
        {
            if (playerController.isMobile())
            {
                SampleWebView.Instance.webViewObject.SetVisibility(true);
                webview1.SetActive(true);
                if (Input.GetKeyDown(KeyCode.X) || SimpleInput.GetButtonDown("X"))
                    webview1.SetActive(false);
            }
            else
            {

                Debug.Log("Third Option");
                // Application.ExternalEval("window.open(\"https://github.com/rohan03122001\")");
                Application.OpenURL("https://github.com/rohan03122001");
            }
        }
        else if (selectedItem == 4)
        {

            if (playerController.isMobile())
            {
                SampleWebView.Instance.webViewObject.SetVisibility(true);
                webview1.SetActive(true);
                if (Input.GetKeyDown(KeyCode.X) || SimpleInput.GetButtonDown("X"))
                    webview1.SetActive(false);
            }
            else
            {
                Debug.Log("Second Option");
                Application.OpenURL("https://rohanbhujbal.vercel.app/assets/resume-example.pdf");
            }

        }
        else if (selectedItem == 5)
        {
            Debug.Log("fifth Option");
            menuController.CloseMenu();
            state = GameState.FreeRoam;
            GameController.instance.interactingWithComputer = false;
        }
        else if (Input.GetKeyDown(KeyCode.X) || SimpleInput.GetButtonDown("X"))
        {
            webview1.SetActive(false);
            menuController.CloseMenu();
            state = GameState.FreeRoam;
            GameController.instance.interactingWithComputer = false;
        }


    }

    IEnumerator Delay()
    {
        Debug.Log("DelayStart");
        
        yield return new WaitForSeconds(1.6f);
        menuController.OpenMenu();
        Debug.Log("DelayEnd");
    }


}
