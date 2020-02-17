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
    }

    void HandlePlayerHealthChanged()
    {
        if (playerHealthFillBar != null)
            playerHealthFillBar.SetValues(0, 1); // player health, player max health.
    }
}
