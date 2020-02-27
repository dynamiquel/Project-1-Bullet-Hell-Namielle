using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : Menu
{
    public void UserInputButtonClicked(int x)
    {
        switch (x)
        {
            case 1:
                Debug.Log("Restart button clicked");
                ButtonClick(0);
                // Restart current level.
                GameManager.Instance.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                Time.timeScale = 1f;
                break;
            case 2:
                Debug.Log("Exit to Main Menu button clicked");
                ButtonClick(0);
                // Return to main menu
                GameManager.Instance.MusicController?.Stop();
                GameManager.Instance.LoadScene("Main Menu");
                break;
        }
    }
}
