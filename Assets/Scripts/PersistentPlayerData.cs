﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersistentPlayerData
{
    // Stores the report of the fastest clear and the highest score clear of every level.
    public Dictionary<string, LevelData> LevelDatas = new Dictionary<string, LevelData>();
    // General leaderboard stats.
    public int Kills;
    public int Deaths;
    public int BulletsShot;
    public int LevelsCompleted;
    public int Score;
    public int AbilitiesUsed;
    public int EnemiesHijacked;
    public long PlayTime;

    // Player data.
    public int PerkPoints;
    // Stores the perks that the player has locked/unlocked.
    public Dictionary<string, bool> PerkUnlocks;
    public string[] ActivePerks = new string[4];

    void AddLevelReport(LevelReport levelReport)
    {
        LevelDatas[levelReport.LevelId].UpdateLevelData(levelReport);
    }

    public PersistentPlayerData(int kills = 0, int deaths = 0, int bulletsShot = 0, int levelsCompleted = 0, int score = 0, int abilitiesUsed = 0, int enemiesHijacked = 0, long playTime = 0, int perkPoints = 0, Dictionary<string, LevelData> levelDatas = null, Dictionary<string, bool> perkUnlocks = null, string[] activePerks = null)
    {
        Kills = kills;
        Deaths = deaths;
        BulletsShot = bulletsShot;
        LevelsCompleted = levelsCompleted;
        Score = score;
        AbilitiesUsed = abilitiesUsed;
        EnemiesHijacked = enemiesHijacked;
        PlayTime = playTime;
        LevelDatas = levelDatas;
        PerkUnlocks = perkUnlocks;
        ActivePerks = activePerks;
    }
}
