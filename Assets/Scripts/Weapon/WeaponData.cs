using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    public string PrefabId { get; set; }

    public string PrimaryBulletPrefabId { get; set; }
    public int PrimaryMaxAmmo { get; set; }
    public int PrimaryClipUsage { get; set; }
    public int PrimaryFireDamage { get; set; }
    public float PrimaryFireVelocity { get; set; }
    public float PrimarySizeModi { get; set; }
    public bool PrimaryExplosive { get; set; }
    public bool Shotgun { get; set; }
    public bool Twinguns { get; set; }

    public string SecondaryBulletPrefabId { get; set; }
    public int SecondaryMaxAmmo { get; set; }
    public int SecondaryClipUsage { get; set; }
    public int SecondaryFireDamage { get; set; }

    public float SecondaryFireVelocity { get; set; }
    public float SecondarySizeModi { get; set; }
    public bool SecondaryExplosive { get; set; }
}
