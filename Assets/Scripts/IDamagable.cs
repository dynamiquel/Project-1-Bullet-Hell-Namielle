using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    int MaxHealth { get; set; }
    int Health { get; set; }

    void OnDeath();

    void OnDamage();

    void Damage(int damage);
}
