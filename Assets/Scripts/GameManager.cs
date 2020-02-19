using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public GameState GameState { get; set; }

    public MusicController musicController;
    [SerializeField] DiscordManager discordManager;

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

    private void OnLevelWasLoaded(int level)
    {
        OnSceneChanged();
    }

    // Just a less redundant way to show the loading screen, unload the current scene and load a new scene.
    public void LoadScene(string sceneName)
    {
        try
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            SceneManager.LoadSceneAsync("Loading Screen", LoadSceneMode.Additive);
            // Unloads the current scene first to save memory.
            SceneManager.UnloadSceneAsync(currentSceneName);
            StartCoroutine(LoadSceneAsync(sceneName));
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Could not load scene '{sceneName}'. Does it exist?");
        }
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            LoadingScreenController.Instance?.SetProgress(Mathf.Clamp01(operation.progress / .9f));
            yield return null;
        }
    }

    void OnSceneChanged()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);

        // If the user is in a playable level.
        if (sceneName.StartsWith("LVL_"))
        {
            GameState = GameState.InGame;
            musicController?.PlayClip(0, AddState.Queue, 0.2f, true, 0.04f, 0.07f);
            discordManager?.SetActivity("In Game", sceneName.Split('_')[1]);
        }
        else
            switch (sceneName)
            {
                case "Title Screen":
                    GameState = GameState.TitleScreen;
                    musicController?.PlayClip(0, AddState.DontReplace, 1f, true, .004f);
                    discordManager?.SetActivity("Title Screen", "Idle");
                    break;
                case "Main Menu":
                    GameState = GameState.MainMenu;
                    musicController?.PlayClip(0, AddState.DontReplace, 1f, true, .05f);
                    discordManager?.SetActivity("Main Menu", "Navigating...");
                    break;
                default:
                    GameState = GameState.Other;
                    discordManager?.SetActivity("The Void", "How did I end up here?");
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
