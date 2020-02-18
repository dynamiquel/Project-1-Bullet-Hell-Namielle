using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiamTester : MonoBehaviour
{
    [SerializeField]
    Enemy boss;
    [SerializeField]
    Enemy playerish;

    // Start is called before the first frame update
    void Start()
    {
        LevelController.Instance.Objective = new Objective(ObjectiveState.New, "Press A!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (LevelController.Instance.CurrentBoss)
                DamageableEntityManager.Instance.DamageEntity(LevelController.Instance.CurrentBoss, 10);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            LevelController.Instance.Objective = new Objective(ObjectiveState.Updated, "Kill Big Nan!");
            LevelController.Instance.CurrentBoss = boss;
        }

        if (Input.GetKeyDown(KeyCode.B))
            DamageableEntityManager.Instance.DamageEntity(playerish, 5);
    }
}
