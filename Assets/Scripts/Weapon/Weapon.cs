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

    public int primaryClipAmmo;
    public int primaryClipMaxAmmo;
    public int secondaryClipAmmo;
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
    private void Awake()
    {
        //Search item database for clip ammo for each weapon and set it here and there useages and there stats
        //Set both bulletPrefab here from database
        Barrel = GameObject.Find("Barrel M").transform;
        SBarrel1 = GameObject.Find("Barrel S1").transform;
        SBarrel2 = GameObject.Find("Barrel S2").transform;
        TBarrel = GameObject.Find("Barrel T1").transform;
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
                bullet2.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi);
                bullet3.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi);
            }
            if (TwinGuns == true)
            {
                GameObject bullet4 = GameObject.Instantiate(primaryBulletPrefab, TBarrel.position, transform.parent.rotation);
                bullet4.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi);
            }
            bullet.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi, primarySizeModi);
            primaryClipAmmo -= primaryClipUseage + ammoConsumptionModi;
        } 
    }

    public void SecondaryFire(int attackModi = 1, int ammoConsumptionModi = 0, float bulletSpeedModi = 1)
    {
        if (secondaryClipAmmo > secondaryClipUseage + ammoConsumptionModi)
        {
            GameObject bullet = GameObject.Instantiate(secondaryBulletPrefab, Barrel.position, transform.parent.rotation);
            bullet.GetComponent<Projectile>().Fired(secondaryFireSpeed * bulletSpeedModi, secondaryFireDamage * attackModi, seccondarySizeModi);
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
