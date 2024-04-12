using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera Camera;
    public GameObject Dialoguebox;

    #region Webgl on mobile check

    [DllImport(dllName: "__Internal")]
    private static extern bool IsMobile();

    public bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
return IsMobile();
#endif
        Debug.Log("not reached");
        return false;
    }

    #endregion
    //public LayerMask solidObjects;
    //public LayerMask interactableLayer;
    //public float moveSpeed;
    //private bool isMoving;
    //private bool isWalkable = true;

    public GameObject mobileController;

    public static PlayerController instance;

    //public event Action<Collider2D> onEnterNPCview;
    private Vector2 input;

    //private Animator animator;
    private Character character;
    private void Awake()
    {
        instance = this;
        //animator = GetComponent<Animator>();
        character = GetComponent<Character>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //up.onClick.AddListener(buttonUP);
        character.Animator.IsMoving = false;
        GameController.instance.state = GameState.cutscene;
        //onEnterNPCview?.Invoke(collider);
        StartCoroutine(TrainerController.instance.TriggerNPC(this));
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        if (isMobile())
        {
            //Dialoguebox.transform.position = new Vector3(Dialoguebox.transform.position.x, Dialoguebox.transform.position.y+50f);
            Camera.orthographicSize = 8;
            mobileController.SetActive(true);

        }
        if (!character.IsMoving)
        {
            //


            //
            input.x = SimpleInput.GetAxisRaw("Horizontal");
            //input.x = Input.GetAxisRaw("Horizontal");
            //input.y = Input.GetAxisRaw("Vertical");
            input.y = SimpleInput.GetAxisRaw("Vertical");
            // input.x = SimpleInput

            //Debug.Log(input);
            if (input.x != 0) input.y = 0;

            if(input != Vector2.zero)
            {
                StartCoroutine(character.Move(input));
                //CheckforFov();
                OnMoveOver();
            }
        }

        character.HandleUpdate();

        if (Input.GetKeyDown(KeyCode.Z) || SimpleInput.GetButtonDown("Z"))
        {
            character.Animator.IsMoving = false;
            Interact();
        }
    }
    
    private void OnMoveOver()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, 0.3f), 0.2f, GameLayers.i.TriggerableLayers);
        
        foreach (var collider in colliders)
        {
            var triggerable = collider.GetComponent<iPlayerTriggerable>();
            if (triggerable != null)
            {
                triggerable.onPlayerTriggered(this);
                break;
            }
        }
        
    }

    void Interact()
    {
        var faceDir = new Vector3(character.Animator.moveX, character.Animator.moveY);
        var interactPos = transform.position + faceDir;

        //Debug.DrawLine(transform.position, interactPos, Color.red, 2f);

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.i.InteractableLayer);
        if(collider!= null)
        {
            collider.GetComponent<Interactable>()?.Interact(transform);
            Debug.Log("Interacting");
        }
    }


    //private void CheckforFov()
    //{
    //    var collider = Physics2D.OverlapCircle(transform.position, 0.2f, GameLayers.i.FovLayer);
    //    if (collider != null)
    //    {
    //        character.Animator.IsMoving = false;
    //        GameController.instance.state = GameState.cutscene;
    //        //onEnterNPCview?.Invoke(collider);
    //        StartCoroutine(TrainerController.instance.TriggerNPC(this));
    //    }
    //}
    public Character Character => character;

    public void buttonUP()
    {

    }
    
}
