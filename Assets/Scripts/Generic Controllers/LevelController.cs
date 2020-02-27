using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }
    public string levelId = "placeholder";

    [SerializeField]
    Enemy _currentBoss;
    public Enemy CurrentBoss
    {
        get => _currentBoss;
        set
        {
            _currentBoss = value;
            OnBossChanged?.Invoke(CurrentBoss);
        }
    }
    public event Action<Enemy> OnBossChanged;

    Objective _objective = new Objective(ObjectiveState.New, "Kill");
    public Objective Objective
    {
        get => _objective;
        set
        {
            _objective = value;
            OnObjectiveChanged?.Invoke(Objective);
        }
    }
    public event Action<Objective> OnObjectiveChanged;

    public PlayerController PlayerController { get; set; }

    [SerializeField] Transform _decalsTransform;
    public Transform DecalsTransform { get => _decalsTransform; }

    public event Action<long> OnScoreChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        // Loads the UI as a seperate scene.
        if (!SceneManager.GetSceneByName("UI").isLoaded)
            SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
    }

    private void Start()
    {
        DamageableEntityManager.Instance.OnEnemyDeath += HandleEnemyDeath;
        DamageableEntityManager.Instance.OnPlayerDeath += HandlePlayerDeath;
        PlayerController.OnTakeover += HandleTakeover;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    public void EndLevel()
    {
        Debug.Log("Ending level...");
        // Save all data
        PlayerController.stats.UpdatePlayerProgress();
        PlayerController.stats.CreateLevelReport();

        // Save perks
        if (PlayerController.PerkController != null)
            foreach (var key in PlayerController.PerkController.ActivePerks.Keys)
                if (!PlayerController.stats.PersistentPlayerData.UnlockedPerks.Contains(key))
                    PlayerController.stats.PersistentPlayerData.UnlockedPerks.Add(key);

        SaveManager.Instance.Save(PlayerController.stats.PersistentPlayerData);

        // Show summary screen
        Debug.Log("Level ended");
    }

    void UpdateTimer()
    {
        // Converts float to double for precision, then rounds back to float.
        PlayerController.stats.PlayTime += Mathf.RoundToInt((float)((double)Time.deltaTime * 1000));
    }

    void HandleEnemyDeath(Enemy entity)
    {
        PlayerController.stats.Kills++;
        PlayerController.stats.Score += entity.Score;
        PlayerController.stats.AddExp(entity.Exp);

        OnScoreChanged?.Invoke(PlayerController.stats.Score);
    }

    void HandlePlayerDeath(Player entity)
    {
        PlayerController.stats.Deaths++;
    }

    void HandleTakeover()
    {
        PlayerController.stats.EnemiesHijacked++;
    }
}
