using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : Menu
{
    protected override void UserInput()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            MainMenuController.Instance.LoadMenu(0);
        }
    }
}
