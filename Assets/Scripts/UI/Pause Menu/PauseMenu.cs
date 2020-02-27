using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    [SerializeField] PauseMenuController pauseMenuController;

    public void UserInputButtonClicked(int x)
    {
        switch (x)
        {
            case 1:
                Debug.Log("Resume button clicked");
                ButtonClick(0);
                pauseMenuController.CloseMenu();
                break;
            case 2:
                Debug.Log("Options button clicked");
                
                ButtonClick(0);
                break;
            case 3:
                Debug.Log("Exit to Main Menu button clicked");
                ButtonClick(0);
                GameManager.Instance.MusicController?.Stop();
                GameManager.Instance.LoadScene("Main Menu");
                break;
        }
    }
}
