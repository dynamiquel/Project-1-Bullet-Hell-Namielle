using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    public GameState GameState { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        OnSceneChanged();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnLevelWasLoaded(int level)
    {
        OnSceneChanged();
    }

    void OnSceneChanged()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        switch (SceneManager.GetActiveScene().name)
        {
            case "Title Screen":
                GameState = GameState.TitleScreen;
                break;
            case "Main Menu":
                GameState = GameState.MainMenu;
                break;
            case "InGame":
                GameState = GameState.InGame;
                break;
            default:
                GameState = GameState.Other;
                break;
        }
    }
}

public enum GameState
{
    TitleScreen,
    MainMenu,
    Loading,
    InGame,
    Other
}
