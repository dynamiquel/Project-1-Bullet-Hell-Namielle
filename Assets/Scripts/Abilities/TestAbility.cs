using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ask Liam for help.
public class TestAbility : Ability
{
    public override string Id { get; set; } = "test";
    public override string Name { get; set; } = "Tester";
    public override float Cooldown { get; set; } = 1f;

    public override bool Use()
    {
        if (!base.Use())
            return false;

        print("Test ability executed");

        return true;
    }
}
