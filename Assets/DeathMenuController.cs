using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuController : MonoBehaviour
{
    public DeathMenuState DeathMenuState { get; private set; } = DeathMenuState.Closed;
    [SerializeField] PauseMenuController pauseMenuController;
    [SerializeField] GameObject deathMenu;
    [SerializeField] GameObject levelCompleteMenu;

    private void Start()
    {
        LevelController.Instance.PlayerController.OnPlayerDeath += HandleOnPlayerDeath;
    }

    private void HandleOnPlayerDeath(Player obj)
    {
        OpenDeathMenu();
    }

    public void CloseMenu()
    {
        deathMenu.SetActive(false);
        levelCompleteMenu.SetActive(false);
        pauseMenuController.PauseGame(false);
        DeathMenuState = DeathMenuState.Closed;
    }

    public void OpenDeathMenu()
    {
        pauseMenuController.PauseGame(true);
        deathMenu.SetActive(true);
        DeathMenuState = DeathMenuState.DeathMenu;
    }

    public void OpenLevelCompleteMenu()
    {
        pauseMenuController.PauseGame(true);
        levelCompleteMenu.SetActive(true);
        DeathMenuState = DeathMenuState.LevelCompleteMenu;
    }
}

public enum DeathMenuState
{
    Closed,
    DeathMenu,
    LevelCompleteMenu
}