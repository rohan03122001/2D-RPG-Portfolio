using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public LayerMask solidObjects;
    //public LayerMask interactableLayer;
    //public float moveSpeed;
    //private bool isMoving;
    //private bool isWalkable = true;


    //public event Action<Collider2D> onEnterNPCview;
    private Vector2 input;

    //private Animator animator;
    private Character character;
    private void Awake()
    {
        //animator = GetComponent<Animator>();
        character = GetComponent<Character>();
    }
    // Start is called before the first frame update
    void Start()
    {
        character.Animator.IsMoving = false;
        GameController.instance.state = GameState.cutscene;
        //onEnterNPCview?.Invoke(collider);
        StartCoroutine(TrainerController.instance.TriggerNPC(this));
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        if (!character.IsMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
        
            if(input.x != 0) input.y = 0;

            if(input != Vector2.zero)
            {
                StartCoroutine(character.Move(input));
                //CheckforFov();
                OnMoveOver();
            }
        }

        character.HandleUpdate();

        if (Input.GetKeyDown(KeyCode.Z))
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
}
