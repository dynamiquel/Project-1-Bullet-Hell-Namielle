using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossSection : HUDComponent
{
    [SerializeField]
    TextMeshProUGUI bossNameText;
    [SerializeField]
    TextMeshProUGUI bossHealthText;
    [SerializeField]
    Slider bossHealthSlider;

    Enemy boundBoss;

    // Start is called before the first frame update
    void Start()
    {
        LevelController.Instance.OnBossChanged += HandleBossChanged;
        gameObject.SetActive(false);
    }

    void HandleBossHealthChanged()
    {
        if (bossHealthText != null)
            bossHealthText.text = boundBoss.Health.ToString();

        if (bossHealthSlider != null)
            bossHealthSlider.value = ((float)boundBoss.Health / boundBoss.MaxHealth);
    }

    void HandleBossChanged(Enemy boss)
    {
        if (boundBoss != null)
            boundBoss.OnHealthChanged -= HandleBossHealthChanged;

        boundBoss = boss;

        if (boundBoss != null)
        {
            gameObject.SetActive(true);
            boundBoss.OnHealthChanged += HandleBossHealthChanged;
            HandleBossHealthChanged();
        }
        else
        {
            gameObject.SetActive(false);
            return;
        }

        if (bossNameText != null)
            bossNameText.text = boundBoss.id;
    }
}
