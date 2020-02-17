using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }

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
    }

    private void Start()
    {
    }
}
