[System.Serializable]
public class LevelReport
{
    public string LevelId { get; set; }
    public int Kills { get; set; }
    public int Deaths { get; set; }
    public int BulletsShot { get; set; }
    public int AbilitiesUsed { get; set; }
    public int EnemiesHijacked { get; set; }
    public bool LevelCompleted { get; set; }
    public long Score { get; set; }
    public long Time { get; set; }

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
