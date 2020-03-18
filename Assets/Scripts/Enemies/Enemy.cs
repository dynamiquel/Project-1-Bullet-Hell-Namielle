using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ask Liam for help.
// Only inherit from this class. Do not actually use this class as a game object.
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour, IDamageable, IScorable
{
    public string id = "default_enemy";

    [SerializeField] private string weaponId = "pistol9";

    [SerializeField] protected string bulletLayerName = "Bullets";
    LayerMask bulletLayerMask;

    [SerializeField] protected int _maxHealth = 100;
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int Health { get; set; }

    [SerializeField] protected int _exp = 1;
    public int Exp { get => _exp; set => _exp = value; }
    [SerializeField] protected int _score = 10;
    public int Score { get => _score; set => _score = value; }

    public event Action<IDamageable> OnHealthChanged;

    public string deathAudioClipId;
    [HideInInspector]
    public AudioSource mainAudioSource;

    public virtual void Awake()
    {
        Health = MaxHealth;
        DamageableEntityManager.Instance.AddEntity(this);
        bulletLayerMask = LayerMask.NameToLayer(bulletLayerName);
        mainAudioSource = GetComponent<AudioSource>();
        
        if (!ItemDatabase.Instance.Weapons.ContainsKey(weaponId))
            weaponId = "pistol9";
        
        Instantiate(ItemDatabase.Instance.Weapons[weaponId], gameObject.transform);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"Collided with '{collision.gameObject.name}'");

        // Perhaps there's a more efficient way.
        if (collision.gameObject.layer == bulletLayerMask)
        {
            int damage = collision.gameObject.GetComponent<Projectile>().dmg;
            DamageableEntityManager.Instance?.DamageEntity(this, damage);
        }
    }

    public virtual void OnDamage()
    {
        OnHealthChanged?.Invoke(this);
    }

    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }

    public void Damage(int damage)
    {
        DamageableEntityManager.Instance.DamageEntity(this, damage);
    }

    protected void OnDestroy()
    {
        if (LevelController.Instance.CurrentBoss == this)
            LevelController.Instance.CurrentBoss = null;
    }
}
