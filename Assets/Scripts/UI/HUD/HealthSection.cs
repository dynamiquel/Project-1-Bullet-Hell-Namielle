﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSection : HUDComponent
{
    [SerializeField] FillBar playerHealthFillBar;
    [SerializeField] UIColourShift[] uics;

    int previousHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        // Subscribe.
        LevelController.Instance.PlayerController.OnHealthChange += HandlePlayerHealthChanged;
        // Initialise (required since UI is in a seperate scene).
        HandlePlayerHealthChanged(LevelController.Instance.PlayerController.GetControlledIDamagable());
    }

    void HandlePlayerHealthChanged(IDamageable entity)
    {
        //Debug.Log("Health changed");

        if (playerHealthFillBar != null)
        {
            gameObject.SetActive(true);
            playerHealthFillBar.SetValues(entity.Health, entity.MaxHealth); // player health, player max health.
        }

        if (entity.Health < previousHealth)
            foreach (var ui in uics)
                ui.StartShift();

        previousHealth = entity.Health; // Probably shouldn't do this, but cba creating an OnEntityDamage event.
    }
}
