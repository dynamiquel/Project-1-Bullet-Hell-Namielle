using System;

[System.Serializable]
public class PlayerStats
{
    public PersistentPlayerData PersistentPlayerData;
    public int Kills = 0;
    public int Deaths = 0;
    public int BulletsShot = 0;
    public int LevelsCompleted = 0;
    public int Score = 0;
    public int AbilitiesUsed = 0;
    public int EnemiesHijacked = 0;
    public long PlayTime = 0;
    public int Level { get => UnityEngine.Mathf.FloorToInt(PersistentPlayerData.Exp / 100f); } // temp calculation
    int previousLevel;

    public event Action<int> OnPlayerLevelUp;

    public PlayerStats(PersistentPlayerData persistentPlayerData)
    {
        PersistentPlayerData = persistentPlayerData;
        previousLevel = Level;
        UnityEngine.Debug.Log(Level);
        UnityEngine.Debug.Log(previousLevel);
    }

    // Call when level is complete so player data is generated.
    // Also call SaveManager.Instance.Save() afterwards to save the data.
    public void CreateLevelReport()
    {
        string levelId = "N/A";

        if (LevelController.Instance)
            levelId = LevelController.Instance.levelId;

        var levelReport = new LevelReport(levelId, Kills, Deaths, BulletsShot, AbilitiesUsed, EnemiesHijacked, true, Score, PlayTime);
        PersistentPlayerData.AddLevelReport(levelReport);
        UpdatePlayerProgress();
    }

    public void UpdatePlayerProgress()
    {
        PersistentPlayerData.Kills += Kills;
        PersistentPlayerData.Deaths += Deaths;
        PersistentPlayerData.BulletsShot += BulletsShot;
        PersistentPlayerData.Score += Score;
        PersistentPlayerData.AbilitiesUsed += AbilitiesUsed;
        PersistentPlayerData.EnemiesHijacked = EnemiesHijacked;
        PersistentPlayerData.PlayTime += PlayTime;
    }

    // Alternate way to add exp to player.
    public void AddExp(int exp)
    {
        PersistentPlayerData.Exp += exp;
        
        if (Level > previousLevel)
        {
            UnityEngine.Debug.Log("Level up!");
            OnPlayerLevelUp?.Invoke(Level);
            previousLevel = Level;
        }
    }
}
