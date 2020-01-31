class EnemyManager
{
    List<Enemy> Enemies;

    void DamageEnemy(Enemy enemy, decimal damage)
    {
        if (Enemies.Contains(enemy))
            Enemies[index of enemy].Damage(damage)
    }

    void RemoveEnemy(Enemy enemy)
    {
        if (Enemies.Contains(enemy))
            Enemies.Remove(enemy);
    }

    void AddEnemy(Enemy enemy)
    {
        if (!Enemies.Contains(enemy))
            Enemies.Add(enemy);
    }
}

class Player
{
    PeristentPlayerData playerData;
    double hijackCooldown;
    Perk[] activePerks = new Perk[4];

    void UsePerk(int perkIndex)
    {
        activePerks[perkIndex].Use();
    }

    void InitalisePlayer()
    {
        // Gives the player it's perks.
        for (int i = 0; i < activePerks.Length; i++)
        {
            string perkId = playerData.activePerks[i];

            if (ItemDatabase.Perks.ContainsKey(perkId))
                activePerks[i] = ItemDatabase.Perks[perkId];
            else
                activePerks[i] = ItemDatabase.Perks["empty_perk"];
        }
    }
}

class PlayerContoller
{
    // Everything that has something to do with controlling the character, i.e: movement, shooting.
    // Store referenced enemy here or Player?
}

// Object that is used to store the player's data.
class PeristentPlayerData
{
    // Stores the report of the fastest clear and the highest score clear of every level.
    Dictionary<string, LevelData> levelDatas;
    // General leaderboard stats.
    int kills;
    int deaths;
    int bulletsShot;
    int levelsCompleted;
    int score;
    int abilitiesUsed;
    int enemiesHijacked;
    long playTime;

    // Player data.
    int perkPoints;
    // Stores the perks that the player has locked/unlocked.
    Dictionary<string, bool> perkUnlocks;
    String[] activePerks = new String[4];

    void AddLevelReport(LevelReport levelReport)
    {
        levelDatas[levelReport.levelId].UpdateLevelData(levelReport);
    }
}

// A report that is generated at the end of every match.
class LevelReport
{
    string levelId;
    int kills;
    int deaths;
    int bulletsShot;
    int abilitiesUsed;
    int enemiesHijacked;
    bool levelCompleted;
    long score;
    long time;
}

// Stores the report of the fastest clear and the highest score clear of the particular level.
// Useful for leaderboards.
class LevelData
{
    string levelId;
    LevelReport highestScoreLevelReport;
    LevelReport lowestTimeScoreLevelReport;

    long GetHighestScore();
    long GetLowestTime();
    void UpdateLevelData(LevelReport levelReport)
    {
        if (levelReport.score > GetHighestScore())
            highestScoreLevelReport = levelReport;
        if (levelReport.time > GetLowestTime())
            lowestTimeScoreLevelReport = levelReport;
    }
}

class Enemy
{
    decimal currentHealth;
    decimal maxHealth;
    bool currentlyHijacked;

    int primaryAmmo;
    int secondaryAmmo;

    Weapon weapon = ItemDatabase.Weapons["weapon_id"];
    List<Ability> abilities = {ItemDatabase.Abilities["ability1_id"], ItemDatabase.Abilities["ability2_id"]};

    void Damage(decimal damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            EnemyManager.RemoveEnemy(this);
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    void InitaliseEnemy();
}

class Weapon
{
    int primaryClipAmmo;
    int secondaryClipAmmo;

    void PrimaryFire();
    void SecondaryFire();
    void PrimaryReload();
    void SecondaryReload();
    void ReloadAll();
}

class ItemDatabase
{
    Dictionary<string, Weapon> Weapons;
    Dictionary<string, Ability> Abilities;
    Dictionary<string, Perk> Perks;
    Dictionary<string, Enemy> Enemies;
}