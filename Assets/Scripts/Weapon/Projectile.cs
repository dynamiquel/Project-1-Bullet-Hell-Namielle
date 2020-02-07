using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    float lifetime = 0;
    int dmg;
    float sizeModifier = 0f;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        
    }

    public void Fired(float speed, int damage, float x)
    {
        dmg = damage;
        rb.velocity = transform.up * speed;
        sizeModifier = x;
    }

    private void FixedUpdate()
    {
        if (lifetime >= 2f)
            Destroy(gameObject);

        lifetime += Time.fixedDeltaTime;
        transform.localScale = new Vector3(3+lifetime* sizeModifier, 3+lifetime* sizeModifier, 3+lifetime* sizeModifier);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //DealDamage
        //Destroy(this);
    }

    

}
