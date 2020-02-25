using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    // Unity Inspector sucks ass. Doesn't even support ArrayLists, so disappointing.
    public ArrayList disabledComponents = new ArrayList();

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject hudCanvas;
    [SerializeField] HubMenuController hubMenuController;
    [SerializeField] DeathMenuController deathMenuController;

    public PauseMenuState PauseMenuState { get; private set; } = PauseMenuState.Closed;

    private void Update()
    {
        // Prevent the pause menu from opening if the death menu is open.
        // Really should have made some menu priority system.
        if (deathMenuController.DeathMenuState != DeathMenuState.Closed)
            return;

        if (Input.GetButtonDown("Pause"))
            switch (PauseMenuState)
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

    public void PauseGame(bool value)
    {
        if (value)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;

            // Adds components to disabled list.
            foreach (var item in FindObjectsOfType<PlayerController>())
                disabledComponents.Add(item);
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Confined;
        }

        // Enables / disables components.
        foreach (var item in disabledComponents)
        {
            MonoBehaviour component = (MonoBehaviour)item;
            component.enabled = !value;
        }

        hudCanvas.SetActive(!value);
    }

    void OpenPauseMenu()
    {
        PauseMenuState = PauseMenuState.Main;

        if (optionsMenu)
            optionsMenu.SetActive(false);
        if (pauseMenu)
            pauseMenu.SetActive(true);

        // Closes the Hub if it's open.
        if (hubMenuController?.HubMenuState != HubMenuState.Closed)
            hubMenuController.CloseMenu();

        PauseGame(true);
    }

    public void OpenOptionsMenu()
    {
        PauseMenuState = PauseMenuState.Options;

        if (pauseMenu)
            pauseMenu.SetActive(false);
        if (optionsMenu)
            optionsMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        PauseMenuState = PauseMenuState.Closed;

        if (pauseMenu)
            pauseMenu.SetActive(false);
        if (optionsMenu)
            optionsMenu.SetActive(false);

        PauseGame(false);
    }
}

public enum PauseMenuState
{
    Closed,
    Main,
    Options
}