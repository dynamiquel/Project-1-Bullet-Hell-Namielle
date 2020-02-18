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
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        
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
        transform.localScale = new Vector3(3+lifetime* sizeModifier, 3+lifetime* sizeModifier, 3+lifetime* sizeModifier);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //DealDamage
        //Destroy(this);
    }

    void endObject()
    {
        print("Running");
        if (explosive)
        {

        }

        Destroy(gameObject);
    }

    

}
