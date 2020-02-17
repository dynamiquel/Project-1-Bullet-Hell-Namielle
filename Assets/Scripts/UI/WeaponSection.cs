using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSection : HUDComponent
{
    [SerializeField] GameObject wpn1Go;
    [SerializeField] TextMeshProUGUI wpn1Curr;
    [SerializeField] TextMeshProUGUI wpn1Res;
    [SerializeField] GameObject wpn2Go;
    [SerializeField] TextMeshProUGUI wpn2Curr;
    [SerializeField] TextMeshProUGUI wpn2Res;

    // Start is called before the first frame update
    void Start()
    {
        // Get the player controller and subscribe to it's event.
        LevelController.Instance.PlayerController.OnWeaponChanged += HandleWeaponChanged;
    }

    void HandleWeaponChanged(Weapon weapon)
    {
        if (weapon)
        {
            wpn1Curr.text = weapon.primaryClipAmmo.ToString();
            wpn1Res.text = weapon.primaryClipMaxAmmo.ToString();

            wpn2Curr.text = weapon.secondaryClipAmmo.ToString();
            wpn2Res.text = weapon.secondaryClipMaxAmmo.ToString();

            if (Visible)
            {
                wpn1Go.SetActive(true);
                wpn2Go.SetActive(true);
            }
        }
        else
        {
            wpn1Go.SetActive(false);
            wpn2Go.SetActive(false);
        }
    }
}
