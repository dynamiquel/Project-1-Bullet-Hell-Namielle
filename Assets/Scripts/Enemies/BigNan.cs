using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigNan : Enemy
{
    private void OnEnable()
    {
        LevelController.Instance.CurrentBoss = this;
    }

    public override void OnDeath()
    {
        Debug.Log("Can't kill me, beach!");
        LevelController.Instance.Objective = new Objective(ObjectiveState.Updated, "Well, you tried!");
        LevelController.Instance.EndLevel();
    }
}
