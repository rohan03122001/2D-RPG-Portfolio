using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] List<TMPro.TextMeshProUGUI> menuItems = new List<TMPro.TextMeshProUGUI>();

    public event Action<int> onMenuSelected;
    public event Action onBack;

    public GameObject moveUp;
    public GameObject ScrollUp;
    public GameObject moveDown;
    public GameObject ScrollDown;

    int selectedItem = 0;
    private void Awake()
    {
        // Get all TextMeshProUGUI components from the children of the menu object and add them to the menuItems list
        foreach (TextMeshProUGUI textComponent in menu.GetComponentsInChildren<TextMeshProUGUI>())
        {
            menuItems.Add(textComponent);
            //ebug.Log(menuItems);
        }
    }

    public void OpenMenu()
    {
        moveUp.SetActive(false);
        ScrollUp.SetActive(true);
        moveDown.SetActive(false);
        ScrollDown.SetActive(true);

        menu.SetActive(true);
        UpdateItemSelection();
    }

    public void CloseMenu()
    {
        moveUp.SetActive(true);
        ScrollUp.SetActive(false);
        moveDown.SetActive(true);
        ScrollDown.SetActive(false);

        menu.SetActive(false);
        GameController.instance.interactingWithComputer = false;
        Computer.instance.screen.SetActive(false);
    }

    public void HandleUpdate()
    {
        int prevSelection = selectedItem;

        if (Input.GetKeyDown(KeyCode.DownArrow) || SimpleInput.GetButtonDown("Down"))
        {

            ++selectedItem;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || SimpleInput.GetButtonDown("Up"))
            --selectedItem;

        selectedItem = Mathf.Clamp(selectedItem,0,menuItems.Count-1);

        if(prevSelection != selectedItem)
            UpdateItemSelection();

        if (Input.GetKeyDown(KeyCode.Z) || SimpleInput.GetButtonDown("Z"))
        {
            onMenuSelected?.Invoke(selectedItem);
        }
        else if (Input.GetKeyDown(KeyCode.X) || SimpleInput.GetButtonDown("X"))
        {
            GameController.instance.interactingWithComputer = false;
            onBack?.Invoke();
            CloseMenu();
        }
    }

    private void UpdateItemSelection()
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            if (i == selectedItem)
            {
                menuItems[i].color = Color.blue;
            }
            else
            {
                menuItems[i].color = Color.black;
            }
        }
    }
}
