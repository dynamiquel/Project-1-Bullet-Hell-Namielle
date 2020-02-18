using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigNan : Enemy
{
    public override void OnDeath()
    {
        Debug.Log("Can't kill me, beach!");
    }
}
