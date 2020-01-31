using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    decimal MaxHealth { get; set; }
    decimal Health { get; set; }

    void OnDeath();

    void OnDamage();

    void Damage(decimal damage);
}
