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
    private void Awake()
    {
        //Search item database for clip ammo for each weapon and set it here and there useages and there stats
        //Set both bulletPrefab here from database
        Barrel = GameObject.Find("Barrel").transform;
    }

    public void PrimaryFire(int attackModi = 1, int ammoConsumptionModi = 0, float bulletSpeedModi = 1)
    {

        if (primaryClipAmmo > primaryClipUseage + ammoConsumptionModi)
        {
            GameObject bullet = GameObject.Instantiate(primaryBulletPrefab, Barrel.position, transform.parent.rotation);
            bullet.GetComponent<Projectile>().Fired(primaryFireSpeed * bulletSpeedModi, primaryFireDamage * attackModi);
            primaryClipAmmo -= primaryClipUseage + ammoConsumptionModi;
        } 
    }

    public void SecondaryFire(int attackModi = 1, int ammoConsumptionModi = 0, float bulletSpeedModi = 1)
    {
        if (secondaryClipAmmo < secondaryClipUseage + ammoConsumptionModi)
        {
            GameObject bullet = GameObject.Instantiate(secondaryBulletPrefab, Barrel.position, Quaternion.identity);
            bullet.GetComponent<Projectile>().Fired(secondaryFireSpeed * bulletSpeedModi, secondaryFireDamage * attackModi);
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
