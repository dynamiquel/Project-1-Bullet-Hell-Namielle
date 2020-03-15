using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ask Liam for help.
public interface IDamageable
{
    int MaxHealth { get; set; }
    int Health { get; set; }

    event Action<IDamageable> OnHealthChanged;

    void OnDeath();

    void OnDamage();

    void Damage(int damage);
}
