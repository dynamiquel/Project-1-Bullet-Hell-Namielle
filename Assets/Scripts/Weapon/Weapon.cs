﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just a general idea
public class Weapon : MonoBehaviour
{
    //Change to private anything that has been added to the item database aside from the id
    public string id;

    [SerializeField] GameObject primaryBulletPrefab;
    [SerializeField] GameObject secondaryBulletPrefab;
    Transform Barrel;
    Transform TBarrel;

    [HideInInspector]
    public int primaryClipAmmo;
    public int primaryClipMaxAmmo;
    [HideInInspector]
    public int secondaryClipAmmo;
    public int secondaryClipMaxAmmo;

    public int primaryClipUseage;
    public int secondaryClipUseage;

    public int primaryFireDamage;
    public float primaryFireSpeed;
    public int secondaryFireDamage;
    public float secondaryFireSpeed;

    public float primarySizeModi = 0.03f;
    public float seccondarySizeModi = 0.01f;

    public int Shotgun = 0;
    public bool TwinGuns = false;

    public int primaryExplosive = 0;
    public int secondaryExplosive = 5;

    // Measured in RPM
    public float primaryFireRate = -1; // -1 = currently no delay, but should be semi-auto.
    public float secondaryFireRate = -1;

    private bool _isPlayer = false;
    public bool IsPlayer
    {
        get => _isPlayer;
        set
        {
            _isPlayer = value;
            SetModifiers();
        }
    }

    [SerializeField] string primaryBulletSoundId;
    [SerializeField] string primaryReloadSoundId;
    [SerializeField] string secondaryBulletSoundId;
    [SerializeField] string secondaryReloadSoundId;

    AudioSource primaryFireAudioSource;
    AudioSource primaryReloadAudioSource;

    Coroutine primaryFireDelay;
    Coroutine secondaryFireDelay;

    private bool primaryReloading = false;
    private bool secondaryReloading = false;
    [SerializeField] private bool autoReload = false;
    [SerializeField] private float primaryReloadDelay = 1f;
    [SerializeField] private int bulletsPerPrimaryReload = -1; // -1 = same as mag size.
    [SerializeField] private float secondaryReloadDelay = 3f;
    [SerializeField] private int bulletsPerSecondaryReload = -1; // -1 = same as mag size.
    
    /*public Weapon(WeaponData weaponData)
    {
        primaryClipMaxAmmo = weaponData.PrimaryMaxAmmo;
        primaryClipUseage = weaponData.PrimaryClipUsage;
        primaryFireDamage = weaponData.PrimaryFireDamage;
        primaryFireSpeed = weaponData.PrimaryFireVelocity;
        //primaryExplosive = weaponData.PrimaryExplosive;
        primaryFireRate = weaponData.PrimaryFireRate;
        //Shotgun = weaponData.Shotgun;
        TwinGuns = weaponData.Twinguns;

        secondaryClipMaxAmmo = weaponData.SecondaryMaxAmmo;
        secondaryClipUseage = weaponData.SecondaryClipUsage;
        secondaryFireDamage = weaponData.SecondaryFireDamage;
        secondaryFireSpeed = weaponData.SecondaryFireVelocity;
        //secondaryExplosive = weaponData.SecondaryExplosive;
        secondaryFireRate = weaponData.SecondaryFireRate;
    }*/
    
    private void Awake()
    {
        //Search item database for clip ammo for each weapon and set it here and there useages and there stats
        //Set both bulletPrefab here from database

        var transforms = GetComponentsInChildren<Transform>();

        foreach(var a in transforms)
        {
            switch (a.name)
            {
                case "Barrel M":
                    Barrel = a;
                    primaryFireAudioSource = Barrel.GetComponent<AudioSource>();
                    break;
                case "Barrel T1":
                    TBarrel = a;
                    break;
                case "Model":
                    primaryReloadAudioSource = a.GetComponent<AudioSource>();
                    break;
            }
        }
    }

    private void Start()
    {
        // Automatically fully loads weapons.
        primaryClipAmmo = primaryClipMaxAmmo;
        secondaryClipAmmo = secondaryClipMaxAmmo;
    }

    private void Update()
    {
        if (autoReload)
            if (primaryClipAmmo <= 0)
                FirstReload();
            if (secondaryClipAmmo <= 0)
                SecondReload();
    }
    
    public bool PrimaryFire(int attackModi = 1, int ammoConsumptionModi = 0, float bulletSpeedModi = 1)
    {
        if (primaryFireDelay != null || primaryReloading)
            return false;

        if (primaryClipAmmo >= primaryClipUseage + ammoConsumptionModi)
        {
            // Moves all projectiles to its own group.
            Transform parentTrans = null;
            if (LevelController.Instance)
                parentTrans = LevelController.Instance.DecalsTransform;

            if (Shotgun > 0)
            {
                int i = 0;

                while (i < Shotgun)
                {
                    Vector3 newRotation = new Vector3(transform.parent.rotation.x, transform.parent.rotation.y, transform.parent.rotation.eulerAngles.z + ((45f / Shotgun) * (float)i) + -12);
                    GameObject bullet = GameObject.Instantiate(primaryBulletPrefab, Barrel.position, Quaternion.Euler(newRotation), parentTrans);
                    bullet.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi, primaryExplosive);
                    if (TwinGuns == true)
                    {
                        GameObject bullet1 = GameObject.Instantiate(primaryBulletPrefab, TBarrel.position, Quaternion.Euler(newRotation), parentTrans);
                        bullet1.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi, primaryExplosive);
                    }
                    i++;
                }
            }

            if (TwinGuns == true && Shotgun == 0)
            {
                if (!TBarrel)
                {
                    Debug.LogWarning($"TBarrel of Weapon '{gameObject.name}' is not set.");
                    goto SkipToFirstBullet;
                }

                GameObject bullet4 = GameObject.Instantiate(primaryBulletPrefab, TBarrel.position, transform.parent.rotation, parentTrans);
                bullet4.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi, primaryExplosive);
            }

            if (TwinGuns == false && Shotgun == 0)
            {
                GameObject bullet = GameObject.Instantiate(primaryBulletPrefab, Barrel.position, transform.parent.rotation, parentTrans);
                bullet.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi, primaryExplosive);
            }
            SkipToFirstBullet:
            
            primaryClipAmmo -= primaryClipUseage + ammoConsumptionModi;

            primaryFireAudioSource.PlayOneShot(AudioDatabase.GetClip(primaryBulletSoundId));
            //print("Pong");
            primaryFireDelay = StartCoroutine(StartPrimaryFireDelay());

            return true;
        }

        return false;
    }

    public void SecondaryFire(int attackModi = 1, int ammoConsumptionModi = 0, float bulletSpeedModi = 1)
    {
        if (secondaryFireDelay != null || secondaryReloading)
            return;

        if (secondaryClipAmmo >= secondaryClipUseage + ammoConsumptionModi)
        {
            // Moves all projectiles to its own group.
            Transform parentTrans = LevelController.Instance.DecalsTransform;

            GameObject bullet = GameObject.Instantiate(secondaryBulletPrefab, Barrel.position, transform.parent.rotation, parentTrans);
            bullet.GetComponent<Projectile>().Fired(secondaryFireSpeed * bulletSpeedModi, secondaryFireDamage * attackModi, seccondarySizeModi, secondaryExplosive);
            secondaryClipAmmo -= secondaryClipUseage + ammoConsumptionModi;
        }

        primaryFireAudioSource.PlayOneShot(AudioDatabase.GetClip(secondaryBulletSoundId));
        secondaryFireDelay = StartCoroutine(StartSecondaryFireDelay());
    }

    public void FirstReload(float reloadSpeedModi = 1)
    {
        float processedReloadDelay = primaryReloadDelay * reloadSpeedModi;
        StartCoroutine(PrimaryReload(processedReloadDelay, bulletsPerPrimaryReload));
    }

    public void SecondReload(float reloadSpeedModi = 1)
    {
        float processedReloadDelay = secondaryReloadDelay * reloadSpeedModi;
        StartCoroutine(SecondaryReload(processedReloadDelay, bulletsPerSecondaryReload));
    }

    public void ReloadAll(float reloadSpeedModi = 1)
    {
        FirstReload(reloadSpeedModi);
        SecondReload(reloadSpeedModi);
    }
    
    void SetModifiers()
    {
        if (IsPlayer)
        {
            // Do player-only stuff
        }
        else
        {
            // Do enemy-only stuff
        }
    }

    // Wasn't sure if updating time was more efficient than coroutine.
    IEnumerator StartPrimaryFireDelay()
    {
        if (primaryFireRate >= 0)
        {
            yield return new WaitForSeconds(60 / primaryFireRate);
            primaryFireDelay = null;
        }
    }

    IEnumerator StartSecondaryFireDelay()
    {
        if (primaryFireRate >= 0)
        {
            yield return new WaitForSeconds(60 / secondaryFireRate);
            secondaryFireDelay = null;
        }
    }

    IEnumerator PrimaryReload(float reloadDelay, int bulletsToReload)
    {
        if (primaryReloading)
            yield break;
        
        primaryReloading = true;
        yield return new WaitForSeconds(reloadDelay);

        if (bulletsToReload == -1)
            bulletsToReload = primaryClipMaxAmmo;
        
        if ((primaryClipAmmo += bulletsToReload) > primaryClipMaxAmmo)
            primaryClipAmmo = primaryClipMaxAmmo;

        if (IsPlayer)
        {
            print("Reloaded");
            LevelController.Instance.PlayerController.WeaponChanged();
        }
        else
        {
            print("No player");
        }

        primaryReloading = false;
    }
    
    IEnumerator SecondaryReload(float reloadDelay, int bulletsToReload)
    {
        if (secondaryReloading)
            yield break;
        
        secondaryReloading = true;
        yield return new WaitForSeconds(reloadDelay);
        
        if (bulletsToReload == -1)
            bulletsToReload = secondaryClipMaxAmmo;
        
        if ((secondaryClipAmmo += bulletsToReload) > secondaryClipMaxAmmo)
            secondaryClipAmmo = secondaryClipMaxAmmo;

        if (IsPlayer)
        {
            print("Reloaded");
            LevelController.Instance.PlayerController.WeaponChanged();
        }
        
        secondaryReloading = false;
    }
}
