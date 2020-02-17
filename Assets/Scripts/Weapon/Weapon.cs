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
    Transform SBarrel1;
    Transform SBarrel2;
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

    float primarySizeModi = 1;
    float seccondarySizeModi = 1;

    public bool Shotgun = false;
    public bool TwinGuns = false;

    public bool primaryExplosive = false;
    public bool secondaryExplosive = false;

    public Weapon(WeaponData weaponData)
    {
        primaryClipMaxAmmo = weaponData.PrimaryMaxAmmo;
        primaryClipUseage = weaponData.PrimaryClipUsage;
        primaryFireDamage = weaponData.PrimaryFireDamage;
        primaryFireSpeed = weaponData.PrimaryFireVelocity;
        primaryExplosive = weaponData.PrimaryExplosive;
        Shotgun = weaponData.Shotgun;
        TwinGuns = weaponData.Twinguns;

        secondaryClipMaxAmmo = weaponData.SecondaryMaxAmmo;
        secondaryClipUseage = weaponData.SecondaryClipUsage;
        secondaryFireDamage = weaponData.SecondaryFireDamage;
        secondaryFireSpeed = weaponData.SecondaryFireVelocity;
        secondaryExplosive = weaponData.SecondaryExplosive;
    }

    private void Awake()
    {
        //Search item database for clip ammo for each weapon and set it here and there useages and there stats
        //Set both bulletPrefab here from database
        foreach(Transform a in transform)
        {
            if (a.name == "Barrel M")
                Barrel = a;
            else if (a.name == "Barrel S1")
                SBarrel1 = a;
            else if (a.name == "Barrel S2")
                SBarrel2 = a;
            else if (a.name == "Barrel T1")
                TBarrel = a;
        }
    }

    public void PrimaryFire(int attackModi = 1, int ammoConsumptionModi = 0, float bulletSpeedModi = 1)
    {

        if (primaryClipAmmo >= primaryClipUseage + ammoConsumptionModi)
        {
            // Moves all projectiles to its own group.
            Transform parentTrans = null;
            if (LevelController.Instance)
                parentTrans = LevelController.Instance.DecalsTransform;

            GameObject bullet = GameObject.Instantiate(primaryBulletPrefab, Barrel.position, transform.parent.rotation, parentTrans);

            if (Shotgun == true)
            {
                if (!SBarrel1 || !SBarrel2)
                {
                    Debug.LogWarning($"SBarrels of Weapon '{gameObject.name}' is not set.");
                    goto SkipToFirstBullet;
                }

                GameObject bullet2 = GameObject.Instantiate(primaryBulletPrefab, SBarrel1.position, SBarrel1.rotation, parentTrans);
                GameObject bullet3 = GameObject.Instantiate(primaryBulletPrefab, SBarrel2.position, SBarrel2.rotation, parentTrans);
                bullet2.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi, primaryExplosive);
                bullet3.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi, primaryExplosive);
            }

            if (TwinGuns == true)
            {
                if (!TBarrel)
                {
                    Debug.LogWarning($"TBarrel of Weapon '{gameObject.name}' is not set.");
                    goto SkipToFirstBullet;
                }

                GameObject bullet4 = GameObject.Instantiate(primaryBulletPrefab, TBarrel.position, transform.parent.rotation, parentTrans);
                bullet4.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi, primaryExplosive);
            }

            SkipToFirstBullet:
            bullet.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi, primaryExplosive);
            primaryClipAmmo -= primaryClipUseage + ammoConsumptionModi;
            print("Pong");
        } 
    }

    public void SecondaryFire(int attackModi = 1, int ammoConsumptionModi = 0, float bulletSpeedModi = 1)
    {
        if (secondaryClipAmmo >= secondaryClipUseage + ammoConsumptionModi)
        {
            // Moves all projectiles to its own group.
            Transform parentTrans = LevelController.Instance.DecalsTransform;

            GameObject bullet = GameObject.Instantiate(secondaryBulletPrefab, Barrel.position, transform.parent.rotation, parentTrans);
            bullet.GetComponent<Projectile>().Fired(secondaryFireSpeed * bulletSpeedModi, secondaryFireDamage * attackModi, seccondarySizeModi, secondaryExplosive);
            secondaryClipAmmo -= secondaryClipUseage + ammoConsumptionModi;
        }
    }

    public void PrimaryReload(float reloadSpeedModi = 1)
    {
        primaryClipAmmo = primaryClipMaxAmmo;
    }

    public void SecondaryReload(float reloadSpeedModi = 1)
    {
        secondaryClipAmmo = secondaryClipMaxAmmo;
    }

    public void ReloadAll(float reloadSpeedModi = 1)
    {
        PrimaryReload(reloadSpeedModi);
        SecondaryReload(reloadSpeedModi);
    }
}
