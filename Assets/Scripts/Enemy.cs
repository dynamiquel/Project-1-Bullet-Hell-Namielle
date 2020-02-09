using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public string id = "default_enemy";

    [SerializeField]
    int _maxHealth = 100;
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int Health { get; set; }

    public event Action OnHealthChanged;

    private void Awake()
    {
        Health = MaxHealth;
        DamageableEntityManager.Instance.AddEntity(this);
    }

    public virtual void OnDamage()
    {
        OnHealthChanged?.Invoke();
    }

    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }

    public void Damage(int damage)
    {
        DamageableEntityManager.Instance.DamageEntity(this, damage);
    }

    void OnDestroy()
    {
        if (LevelController.Instance.CurrentBoss == this)
            LevelController.Instance.CurrentBoss = null;
    }
}
