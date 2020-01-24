[System.Serializable]
public class LevelReport
{
    public string LevelId { get; private set; }
    public int Kills { get; private set; }
    public int Deaths { get; private set; }
    public int BulletsShot { get; private set; }
    public int AbilitiesUsed { get; private set; }
    public int EnemiesHijacked { get; private set; }
    public bool LevelCompleted { get; private set; }
    public long Score { get; private set; }
    public long Time { get; private set; }

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
