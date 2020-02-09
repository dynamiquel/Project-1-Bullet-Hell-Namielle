using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntityManager : MonoBehaviour
{
    public static DamageableEntityManager Instance { get; private set; }

    List<IDamagable> DamageableEntities = new List<IDamagable>();

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

    public void DamageEntity(IDamagable entity, int damage)
    {
        entity.Health -= damage;
        entity.OnDamage();

        if (entity.Health <= 0)
            KillEntity(entity);
    }

    public void KillEntity(IDamagable entity)
    {
        entity.OnDeath();
    }

    public void AddEntity(IDamagable entity)
    {
        if (!DamageableEntities.Contains(entity))
            DamageableEntities.Add(entity);
    }

    public void RemoveEntity(IDamagable entity)
    {
        DamageableEntities.Remove(entity);
    }
}
