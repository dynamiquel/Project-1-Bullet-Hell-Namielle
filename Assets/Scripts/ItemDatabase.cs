using System.Collections;
using System.Collections.Generic;
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
        }
    }

    private void Start()
    {
        // Adds all the inspector weapons to the weapons dictionary and then empties the list.
        foreach (var weapon in _weapons)
        {
            Weapons[weapon.id] = weapon;
        }

        _weapons = null;

        foreach (var ability in _abilities)
        {
            Abilities[ability.id] = ability;
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

    public Dictionary<string, Weapon> Weapons { get; private set; }
    public Dictionary<string, Ability> Abilities { get; private set; }
    public Dictionary<string, Perk> Perks { get; private set; }
    public Dictionary<string, Enemy> Enemies { get; private set; }

    // Easy way to allow game objects to be added through the inspector.
    [SerializeField]
    List<Weapon> _weapons;
    [SerializeField]
    List<Ability> _abilities;
    [SerializeField]
    List<Perk> _perks;
    [SerializeField]
    List<Enemy> _enemies;
}
