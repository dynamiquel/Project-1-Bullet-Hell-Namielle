using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just a general idea
public class Weapon : MonoBehaviour
{
    //Change to private anything that has been added to the item database aside from the id

    public string id;

    public GameObject primaryBulletPrefab;
    public GameObject secondaryBulletPrefab;
    Transform Barrel;
    Transform SBarrel1;
    Transform SBarrel2;
    Transform TBarrel;

    int primaryClipAmmo;
    public int primaryClipMaxAmmo;
    int secondaryClipAmmo;
    public int secondaryClipMaxAmmo;

    public int primaryClipUseage;
    public int secondaryClipUseage;

    public int primaryFireDamage;
    public float primaryFireSpeed;
    public int secondaryFireDamage;
    public float secondaryFireSpeed;

    float primarySizeModi =1;
    float seccondarySizeModi =1;

    public bool Shotgun = false;
    public bool TwinGuns = false;

    public bool primaryExplosive = false;
    public bool seccondaryExplosive = false;
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

        if (primaryClipAmmo > primaryClipUseage + ammoConsumptionModi)
        {
            GameObject bullet = GameObject.Instantiate(primaryBulletPrefab, Barrel.position, transform.parent.rotation);
            if (Shotgun == true)
            {
                GameObject bullet2 = GameObject.Instantiate(primaryBulletPrefab, SBarrel1.position, SBarrel1.rotation);
                GameObject bullet3 = GameObject.Instantiate(primaryBulletPrefab, SBarrel2.position, SBarrel2.rotation);
                bullet2.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi, primaryExplosive);
                bullet3.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi, primaryExplosive);
            }
            if (TwinGuns == true)
            {
                GameObject bullet4 = GameObject.Instantiate(primaryBulletPrefab, TBarrel.position, transform.parent.rotation);
                bullet4.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi, primaryExplosive);
            }
            bullet.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi, primaryExplosive);
            primaryClipAmmo -= primaryClipUseage + ammoConsumptionModi;
            print("Ping");
        } 
    }

    public void SecondaryFire(int attackModi = 1, int ammoConsumptionModi = 0, float bulletSpeedModi = 1)
    {
        if (secondaryClipAmmo > secondaryClipUseage + ammoConsumptionModi)
        {
            GameObject bullet = GameObject.Instantiate(secondaryBulletPrefab, Barrel.position, transform.parent.rotation);
            bullet.GetComponent<Projectile>().Fired(secondaryFireSpeed * bulletSpeedModi, secondaryFireDamage * attackModi, seccondarySizeModi, seccondaryExplosive);
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
