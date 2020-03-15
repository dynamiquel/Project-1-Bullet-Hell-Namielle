using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

// Ask Liam for help.
public class Ability : MonoBehaviour
{
    public virtual string Id { get; set; }
    public virtual string Name { get; set; }
    public virtual float Cooldown { get; set; }
    protected float cooldownTimer = 0;
    protected bool CanUse
    {
        get => cooldownTimer <= float.Epsilon;
    }

    private void Awake()
    {
        gameObject.name = Id;
    }

    // Use the ability.
    public virtual void Use()
    {
        if (cooldownTimer > 0)
            return;

        SetCooldown();
        print($"Used ability: {Name}");
    }

    protected void SetCooldown()
    {
        cooldownTimer = Cooldown;
    }

    protected virtual void Update()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }
}
