using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu
{
    public void UserInputButtonClicked(int x)
    {
        switch (x)
        {
            case 1:
                Debug.Log("Play game button clicked");
                ButtonClick(0);
                break;
            case 2:
                Debug.Log("Options button clicked");
                MainMenuManager.Instance.LoadMenu(1);
                ButtonClick(0);
                break;
            case 3:
                Debug.Log("Exit game button clicked");
                Application.Quit();
                ButtonClick(0);
                break;
        }
    }
}
