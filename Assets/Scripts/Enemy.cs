using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public string id;

    [SerializeField]
    decimal _maxHealth = 100;
    public decimal MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public decimal Health { get; set ; }

    private void Awake()
    {
        Health = MaxHealth;
        DamageableEntityManager.Instance.AddEntity(this);
    }

    public virtual void OnDamage()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnDeath()
    {
        throw new System.NotImplementedException();
    }

    public void Damage(decimal damage)
    {
        DamageableEntityManager.Instance.DamageEntity(this, damage);
    }
}
