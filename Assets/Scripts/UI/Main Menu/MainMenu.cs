using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void UserInputButtonClicked(int x)
    {
        switch (x)
        {
            case 1:
                Debug.Log("Play game button clicked");
                ButtonClick(0);
                GameManager.Instance?.LoadScene("LVL_Game v2");
                break;
            case 2:
                Debug.Log("Options button clicked");
                ButtonClick(0);
                MainMenuController.Instance.LoadMenu(1);
                break;
            case 3:
                Debug.Log("Exit game button clicked");
                ButtonClick(0);
                Application.Quit();
                Debug.LogError("Application exited!");
                break;
        }
    }
}
