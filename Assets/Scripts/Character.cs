using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Character : MonoBehaviour
{

    public static Character Instance { get; private set; }
    CharacterAnimator animator;
    public float moveSpeed;

    public bool IsMoving { get; private set; }


    private void Awake()
    {
        Instance = this;
        animator = GetComponent<CharacterAnimator>();
        setPositionSnapToTile(transform.position);
    }

    public void setPositionSnapToTile(Vector2 pos)
    {
        pos.x = Mathf.Floor(pos.x) + 0.5f;
        pos.y = Mathf.Floor(pos.y) + 0.5f;

        transform.position = pos;
    }

    public IEnumerator Move(Vector2 moveVec)
    {
        animator.moveX= Mathf.Clamp(moveVec.x, -1f, 1f);
        animator.moveY= Mathf.Clamp(moveVec.y,-1f,1f);

        var targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;

        if(!IsPathWalkable(targetPos))
            yield break;

        IsMoving=true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = (Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime));
            yield return null;
        }
        transform.position = targetPos;
        IsMoving= false;    
    }

    public void HandleUpdate()
    {
        animator.IsMoving = IsMoving;
    }

    private bool IsPathWalkable(Vector3 targetPos)
    {
        var diff = targetPos-transform.position;
        var dir = diff.normalized;
        if (Physics2D.BoxCast(transform.position + dir, new Vector2(0.2f, 0.2f), 0f, dir, diff.magnitude - 1, GameLayers.i.SolidLayer | GameLayers.i.InteractableLayer | GameLayers.i.PlayerLayer) == true)
            return false;
       
        return true;
    
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.01f, GameLayers.i.SolidLayer | GameLayers.i.InteractableLayer) != null)
        {
            return false;
        }
        return true;
    }

    public void LookTowards(Vector2 targetPos)
    {
        var xDif = Mathf.Floor(targetPos.x) - Mathf.Floor(transform.position.x);
        var yDif = Mathf.Floor(targetPos.y) - Mathf.Floor(transform.position.y);

        if(xDif==0 || yDif == 0)
        {
            animator.moveX = Mathf.Clamp(xDif, -1f, 1f);
            animator.moveY = Mathf.Clamp(yDif, -1f, 1f);
        }
    }

    public CharacterAnimator Animator
    {
        get => animator;
    }
}
