using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSection : HUDComponent
{
    [SerializeField]
    FillBar playerHealthFillBar;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        // Subscribe.
        LevelController.Instance.PlayerController.OnHealthChange += HandlePlayerHealthChanged;
        // Initialise (required since UI is in a seperate scene).
        HandlePlayerHealthChanged(LevelController.Instance.PlayerController.GetControlledIDamagable());
    }

    void HandlePlayerHealthChanged(IDamagable entity)
    {
        Debug.Log("Health changed");
        if (playerHealthFillBar != null)
        {
            gameObject.SetActive(true);
            playerHealthFillBar.SetValues(entity.Health, entity.MaxHealth); // player health, player max health.
        }
    }
}
