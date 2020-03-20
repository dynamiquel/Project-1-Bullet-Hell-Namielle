using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityAbility : Ability
{
    public override string Id { get; set; } = "invincibility";
    public override string Name { get; set; } = "Invincibility";
    public override float Cooldown { get; set; } = 15f;

    public override bool Use()
    {
        if (!base.Use())
            return false;
        
        StartCoroutine(Execute());
        print("Invincibility ability executed");
        
        return true;
    }

    IEnumerator Execute()
    {
        var enemy = transform.parent.GetComponentInParent<Enemy>();

        if (enemy == null)
        {
            print("Could not find enemy component");
            yield break;
        }

        // Sets health to highest possible value.
        enemy.Health = int.MaxValue;
        // Updates UI.
        enemy.InvokeHealthChange();
        
        yield return new WaitForSeconds(4f);

        // Sets health to max health.
        enemy.Health = enemy.MaxHealth;
        enemy.InvokeHealthChange();
    }
}
