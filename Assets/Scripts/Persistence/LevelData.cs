[System.Serializable]
public class LevelData
{
    public string LevelId;
    public LevelReport HighestScoreLevelReport;
    public LevelReport LowestTimeScoreLevelReport;

    public long HighestScore { get => HighestScoreLevelReport.Score; }

    public long LowestTime { get => LowestTimeScoreLevelReport.Time; }

    public LevelData(string levelId, LevelReport highestScoreLevelReport, LevelReport lowestTimeScoreLevelReport)
    {
        LevelId = levelId;
        HighestScoreLevelReport = highestScoreLevelReport;
        LowestTimeScoreLevelReport = lowestTimeScoreLevelReport;
    }

    public void UpdateLevelData(LevelReport levelReport)
    {
        if (levelReport.Score > HighestScore)
            HighestScoreLevelReport = levelReport;
        if (levelReport.Time > LowestTime)
            LowestTimeScoreLevelReport = levelReport;
    }


}
