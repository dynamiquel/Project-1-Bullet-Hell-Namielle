using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsMenu;
    PauseMenuState pauseMenuState = PauseMenuState.Closed;

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
            switch (pauseMenuState)
            {
                case PauseMenuState.Closed:
                    OpenPauseMenu();
                    break;
                case PauseMenuState.Main:
                    CloseMenu();
                    break;
                case PauseMenuState.Options:
                    OpenOptionsMenu();
                    break;
            }
    }

    void OpenPauseMenu()
    {
        pauseMenuState = PauseMenuState.Main;

        if (optionsMenu)
            optionsMenu.SetActive(false);
        if (pauseMenu)
            pauseMenu.SetActive(true);
    }

    public void OpenOptionsMenu()
    {
        pauseMenuState = PauseMenuState.Options;

        if (pauseMenu)
            pauseMenu.SetActive(false);
        if (optionsMenu)
            optionsMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        pauseMenuState = PauseMenuState.Closed;

        if (pauseMenu)
            pauseMenu.SetActive(false);
        if (optionsMenu)
            optionsMenu.SetActive(false);
    }
}

public enum PauseMenuState
{
    Closed,
    Main,
    Options
}