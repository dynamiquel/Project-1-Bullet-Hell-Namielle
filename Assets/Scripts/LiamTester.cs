using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiamTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelController.Instance.Objective = new Objective(ObjectiveState.New, "Find Big Nan!");
    }

    private void Update()
    {
        // Damages the player.
        if (Input.GetKeyDown(KeyCode.K))
            DamageableEntityManager.Instance.DamageEntity(LevelController.Instance.PlayerController.GetControlledIDamagable(), 10);
    }
}
