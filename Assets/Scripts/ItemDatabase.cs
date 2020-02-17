using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

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
    }

    public Dictionary<string, Weapon> Weapons { get; private set; } = new Dictionary<string, Weapon>();
    public Dictionary<string, Ability> Abilities { get; private set; } = new Dictionary<string, Ability>();
    public Dictionary<string, Perk> Perks { get; private set; } = new Dictionary<string, Perk>();
    public Dictionary<string, Enemy> Enemies { get; private set; } = new Dictionary<string, Enemy>();

    // Easy way to allow game objects to be added through the inspector.
    [SerializeField]
    List<Weapon> _weapons;
    [SerializeField]
    List<Ability> _abilities;
    [SerializeField]
    List<Perk> _perks;
    [SerializeField]
    List<Enemy> _enemies;

    private void Start()
    {
        AddInspectorItems();

        ReadWeaponJson();
    }

    // Adds all the inspector weapons to the weapons dictionary and then empties the list.
    void AddInspectorItems()
    {
        foreach (var weapon in _weapons)
        {
            Weapons[weapon.id] = weapon;
        }

        _weapons = null;

        foreach (var ability in _abilities)
        {
            Abilities[ability.Id] = ability;
        }

        _abilities = null;

        foreach (var perk in _perks)
        {
            Perks[perk.id] = perk;
        }

        _perks = null;

        foreach (var enemy in _enemies)
        {
            Enemies[enemy.id] = enemy;
        }

        _enemies = null;
    }

    // Creates weapon data from the weapons.json file.
    void ReadWeaponJson()
    {
        string path = System.IO.Path.Combine(Application.persistentDataPath, "resources", "weapons.json");

        string json = System.IO.File.ReadAllText(path);

        var weaponsData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, WeaponData>>(json);

        foreach (var weapon in weaponsData)
        {
            CreateWeapon(weapon.Key, weapon.Value);
        }
    }

    // Creates a weapon with the given weapon data.
    void CreateWeapon(string id, WeaponData weaponData)
    {
        Weapon weapon = new Weapon(weaponData);

        /// TODO:
        /// Gets a particular bullet prefab with the weaponData.PrimaryBulletPrefabId.
        /// Gets a particular 'base model' with the weaponData.prefabId.

        // Adds the newly created weapon to the Weapons dictionary.
        Weapons[id] = weapon;
    }
}
