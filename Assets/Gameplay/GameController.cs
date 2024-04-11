using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum GameState { FreeRoam, Dialog , Pause, Menu, cutscene}


public class GameController : MonoBehaviour
{
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

            if (Input.GetKeyDown(KeyCode.Z) && interactingWithComputer)
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
            Debug.Log("First Option");
            Application.OpenURL("https://rohanbhujbal.vercel.app/");
        }else if(selectedItem == 1)
        {
            Debug.Log("Second Option");
            Application.OpenURL("https://rohanbhujbal.vercel.app/assets/resume-example.pdf");
        }
        else if (selectedItem == 2)
        {
            Debug.Log("Third Option");
            Application.OpenURL("https://github.com/rohan03122001");
        }
        else if (selectedItem == 3)
        {
            Debug.Log("fourth Option");
            menuController.CloseMenu();
            state = GameState.FreeRoam;
            GameController.instance.interactingWithComputer = false;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
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
