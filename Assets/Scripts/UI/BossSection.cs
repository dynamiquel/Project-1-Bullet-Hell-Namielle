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
        gameObject.SetActive(false);
    }

    void HandleBossHealthChanged()
    {
        if (bossHealthFillBar != null)
            bossHealthFillBar.Value = boundBoss.Health;
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
            bossHealthFillBar.MaxValue = boundBoss.MaxHealth;
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
