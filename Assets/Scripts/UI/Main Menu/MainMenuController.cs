using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance { get; private set; }

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject levelMenu;

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
            case 2:
                OpenLevelMenu();
                break;
        }
    }

    void OpenMainMenu()
    {
        optionsMenu.SetActive(false);
        levelMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void OpenOptionsMenu()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    void OpenLevelMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        levelMenu.SetActive(true);
    }
}
