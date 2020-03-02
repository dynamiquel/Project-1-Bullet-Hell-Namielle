using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int dmg;
    Rigidbody2D rb;
    float lifetime = 0;
    float sizeModifier = 0f;
    bool explosive = false;
    GameObject bulleta;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        bulleta = this.gameObject;
    }

    public void Fired(float speed, int damage, float x, bool _explosive)
    {
        dmg = damage;
        rb.velocity = transform.up * speed;
        sizeModifier = x;
        explosive = _explosive;
    }

    private void FixedUpdate()
    {
        if (lifetime >= 2f)
            endObject();

        lifetime += Time.fixedDeltaTime;
        this.transform.localScale += new Vector3(lifetime * sizeModifier, lifetime * sizeModifier, lifetime * sizeModifier);
        print(lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        endObject();
    }

    void endObject()
    {
        print("Running");
        if (explosive)
        {
            int i = 0;
            while (i < 8)
            {
                Vector3 newRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + (45 * i));
                GameObject bullet = GameObject.Instantiate(bulleta, transform.position, Quaternion.Euler(newRotation));
                bullet.GetComponent<Projectile>().Fired(2 * 1, dmg * 1, 0.01f, false);
                i++;
            } 
        }

        Destroy(gameObject);
    }

    

}
