using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    [SerializeField] List<Sprite> WalkDownSprites;
    [SerializeField] List<Sprite> WalkUpSprites;
    [SerializeField] List<Sprite> WalkRightSprites;
    [SerializeField] List<Sprite> WalkLeftSprites;

    //Paramerters
    public float moveX {  get; set; }   
    public float moveY { get; set; }
    public bool IsMoving { get; set; }
    //States

    SpriteAnimator walkDownAnim;
    SpriteAnimator walkUpAnim;
    SpriteAnimator walkRightAnim;
    SpriteAnimator walkLeftAnim;

    SpriteAnimator currentAnim;

    SpriteRenderer spriteRenderer;
    bool wasPrevMoving;
    private void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        walkDownAnim = new SpriteAnimator(WalkDownSprites, spriteRenderer);
        walkUpAnim = new SpriteAnimator(WalkUpSprites, spriteRenderer);
        walkRightAnim = new SpriteAnimator(WalkRightSprites, spriteRenderer);
        walkLeftAnim = new SpriteAnimator(WalkLeftSprites, spriteRenderer);

        currentAnim = walkDownAnim;
    }

    private void Update()
    {
        var prevAnim = currentAnim;

        if (moveX == 1)
        {
            currentAnim = walkRightAnim;
        }else if (moveX == -1)
        {
            currentAnim = walkLeftAnim;
        }else if(moveY == 1)
        {
            currentAnim = walkUpAnim;
        }else if(moveY == -1){
            currentAnim = walkDownAnim;
        }

        if (currentAnim != prevAnim || IsMoving != wasPrevMoving)
            currentAnim.Start();

        if (IsMoving)
            currentAnim.HandleUpdate();
        else
            spriteRenderer.sprite = currentAnim.Frames[0];

        wasPrevMoving = IsMoving;
    }
}
