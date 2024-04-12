using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BUTTONUP : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    bool isPressed;

     void Update()
    {
        if (isPressed)
        {
            PlayerController.instance.buttonUP();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }
}
