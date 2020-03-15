using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAbility : Ability
{
    public override string Id { get; set; } = "test";
    public override string Name { get; set; } = "Tester";
    public override float Cooldown { get; set; } = 1f;

    public override void Use()
    {
        base.Use();
        
        if (!CanUse)
            return;
        
        print("Test ability executed");
    }
}
