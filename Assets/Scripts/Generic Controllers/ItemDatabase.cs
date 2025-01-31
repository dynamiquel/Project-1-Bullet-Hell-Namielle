﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Ask Liam for help.
public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }
    public bool Loaded { get; private set; }

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

        Init();
    }
    
    public Dictionary<string, GameObject> Weapons { get; private set; } = new Dictionary<string, GameObject>();
    public Dictionary<string, WeaponData> WeaponDatas { get; private set; } = new Dictionary<string, WeaponData>();
    public Dictionary<string, GameObject> Abilities { get; private set; } = new Dictionary<string, GameObject>();
    public Dictionary<string, PerkData> PerkDatas { get; private set; } = new Dictionary<string, PerkData>();
    public Dictionary<string, Enemy> Enemies { get; private set; } = new Dictionary<string, Enemy>();
    public Dictionary<string, LevelSelectData> LevelDatas { get; private set; } = new Dictionary<string, LevelSelectData>();


    // Easy way to allow game objects to be added through the inspector.
    [SerializeField] List<GameObject> _weapons;
    [SerializeField] List<GameObject> _abilities;
    [SerializeField] List<Perk> _perks;
    [SerializeField] List<Enemy> _enemies;

    private void Init()
    {
        AddInspectorItems();

        StartCoroutine(ReadWeaponJson());
        StartCoroutine(ReadPerksJson());
        StartCoroutine(ReadLevelsJson());

        Loaded = true;
    }

    // Adds all the inspector weapons to the weapons dictionary and then empties the list.
    void AddInspectorItems()
    {
        foreach (var weapon in _weapons)
        {
            var wep = weapon.GetComponent<Weapon>();
            
            if (wep != null)
                Weapons[wep.id] = weapon;
        }

        _weapons = null;

        foreach (var ability in _abilities)
        {
            Ability abil = ability.GetComponent<Ability>();
            
            if (abil != null)
                Abilities[abil.Id] = ability;
        }

        _abilities = null;

        /*foreach (var perk in _perks)
        {
            Perks[perk.id] = perk;
        }*/

        _perks = null;

        foreach (var enemy in _enemies)
        {
            Enemies[enemy.id] = enemy;
        }

        _enemies = null;
    }

    // Creates weapon data from the weapons.json file.
    IEnumerator ReadWeaponJson()
    {
        var request = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/JSON/Weapons.json");
        yield return request.SendWebRequest();
        string json = request.downloadHandler.text;


        var weaponsData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, WeaponData>>(json);

        foreach (var weapon in weaponsData)
        {
            //CreateWeapon(weapon.Key, weapon.Value);
            WeaponDatas[weapon.Key] = weapon.Value;
        }
    }

    IEnumerator ReadPerksJson()
    {
        var request = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/JSON/Perks.json");
        yield return request.SendWebRequest();
        string json = request.downloadHandler.text;


        var perksData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, PerkData>>(json);

        var sb = new System.Text.StringBuilder("PerkDatas loaded:\n");

        foreach (var perkData in perksData)
        {
            PerkDatas[perkData.Key] = perkData.Value;

            sb.AppendLine(perkData.Key);
        }

        Debug.Log(sb.ToString());
    }

    IEnumerator ReadLevelsJson()
    {
        var request = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/JSON/Levels.json");
        yield return request.SendWebRequest();
        string json = request.downloadHandler.text;


        var levelsData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, LevelSelectData>>(json);

        var sb = new System.Text.StringBuilder("LevelDatas loaded:\n");

        foreach (var levelData in levelsData)
        {
            LevelDatas[levelData.Key] = levelData.Value;

            sb.AppendLine(levelData.Key);
        }

        Debug.Log(sb.ToString());
    }

    // Creates a weapon with the given weapon data.
    /*void CreateWeapon(string id, WeaponData weaponData)
    {
        Weapon weapon = new Weapon(weaponData);

        /// TODO:
        /// Gets a particular bullet prefab with the weaponData.PrimaryBulletPrefabId.
        /// Gets a particular 'base model' with the weaponData.prefabId.

        // Adds the newly created weapon to the Weapons dictionary.
        Weapons[id] = weapon;
    }*/
}
