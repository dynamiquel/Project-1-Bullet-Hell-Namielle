using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int dmg;
    Rigidbody2D rb;
    float lifetime = 0;
    float sizeModifier = 0f;
    int explosive = 0;
    GameObject bulleta;
    Transform ProjectileTransform;
    float MaxLifeTime = 2f;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        ProjectileTransform = transform;
        bulleta = this.gameObject;
    }

    public void Fired(float speed, int damage, float x, int _explosive)
    {
        dmg = damage;
        rb.velocity = transform.up * speed;
        sizeModifier = x;
        explosive = _explosive;
    }

    private void FixedUpdate()
    {
        if (lifetime >= MaxLifeTime)
            EndObject();

        lifetime += Time.fixedDeltaTime;
        ProjectileTransform.localScale += new Vector3(lifetime * sizeModifier, lifetime * sizeModifier, lifetime * sizeModifier);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EndObject();
    }

    void EndObject()
    {

        if (explosive > 0)
        {
            int i = 0;
            while (i < explosive)
            {
                Vector3 newRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + ((360 / explosive) * i));
                GameObject bullet = GameObject.Instantiate(bulleta, transform.position, Quaternion.Euler(newRotation));
                bullet.GetComponent<Projectile>().Fired(2 * 1, dmg * 1, 0.01f, 0);
                i++;
            } 
        }

        Destroy(gameObject);
    }

    

}
