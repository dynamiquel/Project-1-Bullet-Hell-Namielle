using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hasn't been implemented.
public class WeaponData
{
    public string PrefabId { get; set; }

    public string PrimaryBulletPrefabId { get; set; }
    public string PrimaryBulletSound { get; set; }
    public string PrimaryReloadSound { get; set; }
    public int PrimaryMaxAmmo { get; set; } = 5;
    public int PrimaryClipUsage { get; set; } = 1;
    public int PrimaryFireDamage { get; set; } = 1;
    public float PrimaryFireVelocity { get; set; } = 1;
    public float PrimarySizeModi { get; set; } = 1;
    public bool PrimaryExplosive { get; set; } = false;
    public float PrimaryFireRate { get; set; } = 60;
    public bool Shotgun { get; set; } = false;
    public bool Twinguns { get; set; } = false;

    public string SecondaryBulletPrefabId { get; set; }
    public string SecondaryBulletSound { get; set; }
    public string SecondaryReloadSound { get; set; }
    public int SecondaryMaxAmmo { get; set; } = 5;
    public int SecondaryClipUsage { get; set; } = 1;
    public int SecondaryFireDamage { get; set; } = 1;
    public float SecondaryFireVelocity { get; set; } = 1;
    public float SecondarySizeModi { get; set; } = 1;
    public bool SecondaryExplosive { get; set; } = false;
    public float SecondaryFireRate { get; set; } = 60;
}
