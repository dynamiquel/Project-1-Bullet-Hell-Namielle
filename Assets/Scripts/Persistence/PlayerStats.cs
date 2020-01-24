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

    public PlayerStats(PersistentPlayerData persistentPlayerData)
    {
        PersistentPlayerData = persistentPlayerData;
    }

    // Call when level is complete so player data is generated.
    // Also call SaveManager.Instance.Save() afterwards to save the data.
    public void CreateLevelReport()
    {
        var levelReport = new LevelReport("TestLevel", Kills, Deaths, BulletsShot, AbilitiesUsed, EnemiesHijacked, true, Score, PlayTime);
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
}
