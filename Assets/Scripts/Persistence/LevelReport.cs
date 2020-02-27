[System.Serializable]
public class LevelReport
{
    public string LevelId { get; set; } = "N/A";
    public int Kills { get; set; } = 0;
    public int Deaths { get; set; } = 0;
    public int BulletsShot { get; set; } = 0;
    public int AbilitiesUsed { get; set; } = 0;
    public int EnemiesHijacked { get; set; } = 0;
    public bool LevelCompleted { get; set; } = false;
    public long Score { get; set; } = 0;
    public long Time { get; set; } = 0;

    public LevelReport()
    {

    }

    public LevelReport(string levelId, int kills, int deaths, int bulletsShot, int abilitiesUsed, int enemiesHijacked, bool levelCompleted, long score, long time)
    {
        LevelId = levelId;
        Kills = kills;
        Deaths = deaths;
        BulletsShot = bulletsShot;
        AbilitiesUsed = abilitiesUsed;
        EnemiesHijacked = enemiesHijacked;
        LevelCompleted = levelCompleted;
        Score = score;
        Time = time;
    }
}
