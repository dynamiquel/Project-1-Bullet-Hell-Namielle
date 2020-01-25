using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    float lifetime = 10;
    int dmg;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        
    }

    public void Fired(float speed, int damage)
    {
        dmg = damage;
        rb.velocity = transform.up * speed;
    }

    private void FixedUpdate()
    {
        if (lifetime <= 0)
        {
            Destroy(this);
        }

        lifetime -= Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //DealDamage
        Destroy(this);
    }

}
