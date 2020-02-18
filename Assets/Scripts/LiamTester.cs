using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiamTester : MonoBehaviour
{
    [SerializeField]
    BigNan boss;
    [SerializeField]
    Enemy playerish;

    // Start is called before the first frame update
    void Start()
    {
        LevelController.Instance.Objective = new Objective(ObjectiveState.New, "Press F1!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (LevelController.Instance.CurrentBoss)
                DamageableEntityManager.Instance.DamageEntity(LevelController.Instance.CurrentBoss, 10);
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            LevelController.Instance.Objective = new Objective(ObjectiveState.Updated, "Kill Big Nan!");
            
        }

        if (Input.GetKeyDown(KeyCode.F3))
            DamageableEntityManager.Instance.DamageEntity(playerish, 5);
    }
}
