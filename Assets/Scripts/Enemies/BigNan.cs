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
        Destroy(gameObject);
        LevelController.Instance.Objective = new Objective(ObjectiveState.Completed	, "You have killed the boss!");
        LevelController.Instance.EndLevel();
    }
}
