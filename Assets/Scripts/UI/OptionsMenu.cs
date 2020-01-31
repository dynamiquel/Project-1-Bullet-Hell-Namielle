using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : Menu
{
    public override void UserInput()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            MainMenuManager.Instance.LoadMenu(0);
        }
    }
}
