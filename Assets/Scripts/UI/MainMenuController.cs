using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance { get; private set; }

    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject optionsMenu;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        LoadMenu(0);
    }

    public void LoadMenu(int ID)
    {
        switch (ID)
        {
            case 0:
                OpenMainMenu();
                break;
            case 1:
                OpenOptionsMenu();
                break;
        }
    }

    void OpenMainMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void OpenOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
}
