using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ask Liam for help.
public class DamageableEntityManager : MonoBehaviour
{
    public static DamageableEntityManager Instance { get; private set; }

    public event Action<Enemy> OnEnemyDeath;
    public event Action<Player> OnPlayerDeath;

    List<IDamageable> DamageableEntities = new List<IDamageable>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void DamageEntity(IDamageable entity, int damage)
    {
        entity.Health -= damage;
        entity.OnDamage();

        if (entity.Health <= 0)
            KillEntity(entity);
    }

    public void KillEntity(IDamageable entity)
    {
        if (entity is Enemy)
            OnEnemyDeath?.Invoke((Enemy)entity);
        else if (entity is Player)
        {
            Debug.Log("Player died");
            OnPlayerDeath?.Invoke((Player)entity);
        }
            

        RemoveEntity(entity);

        entity.OnDeath();

        print($"Enemies remaining: {DamageableEntities.Count}");
    }

    public void AddEntity(IDamageable entity)
    {
        if (!DamageableEntities.Contains(entity))
            DamageableEntities.Add(entity);

        print($"Enemies remaining: {DamageableEntities.Count}");
    }

    public void RemoveEntity(IDamageable entity)
    {
        DamageableEntities.Remove(entity);
    }
}
