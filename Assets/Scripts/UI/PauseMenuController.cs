using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    // Unity Inspector sucks ass. Doesn't even support ArrayLists, so disappointing.
    public ArrayList disabledComponents = new ArrayList();

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

        PauseGame(true);
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

        PauseGame(false);
    }

    void PauseGame(bool value)
    {
        if (value)
        {
            Time.timeScale = 0;

            // Adds components to disabled list.
            foreach (var item in FindObjectsOfType<PlayerController>())
                disabledComponents.Add(item);
        }
        else
            Time.timeScale = 1;

        // Enables / disables components.
        foreach (var item in disabledComponents)
        {
            MonoBehaviour component = (MonoBehaviour)item;
            component.enabled = !value;
        }
    }
}

public enum PauseMenuState
{
    Closed,
    Main,
    Options
}