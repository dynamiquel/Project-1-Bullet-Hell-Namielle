using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ask Liam for help.
public class NullAbility : Ability
{
    public override string Id { get; set; } = "nullAbility";
    public override string Name { get; set; } = "";
    public override float Cooldown { get; set; } = float.PositiveInfinity;

    public override bool Use()
    {
        // This is a null ability. It's not supposed to do anything.
        // It's more performant than comparing to null.
        return false;
    }
}
