using UnityEngine;
using TMPro;

public class BossSection : HUDComponent
{
    [SerializeField]
    TextMeshProUGUI bossNameText;
    [SerializeField]
    FillBar bossHealthFillBar;

    Enemy boundBoss;

    // Start is called before the first frame update
    void Start()
    {
        LevelController.Instance.OnBossChanged += HandleBossChanged;
        HandleBossChanged(LevelController.Instance.CurrentBoss);
        gameObject.SetActive(false);
    }

    void HandleBossHealthChanged(IDamageable health)
    {
        if (bossHealthFillBar != null)
            bossHealthFillBar.Value = health.Health;
    }

    void HandleBossChanged(Enemy boss)
    {
        if (boundBoss != null)
            boundBoss.OnHealthChanged -= HandleBossHealthChanged;

        boundBoss = boss;

        if (boundBoss != null)
        {
            if (Visible)
                gameObject.SetActive(true);

            boundBoss.OnHealthChanged += HandleBossHealthChanged;
            bossHealthFillBar.MaxValue = boundBoss.MaxHealth;
            HandleBossHealthChanged(boss);
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
