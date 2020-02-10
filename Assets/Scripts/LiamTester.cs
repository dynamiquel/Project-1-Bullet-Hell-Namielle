using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiamTester : MonoBehaviour
{
    [SerializeField]
    Enemy boss;

    // Start is called before the first frame update
    void Start()
    {
        LevelController.Instance.Objective = new Objective(ObjectiveState.New, "Kill Everything!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (LevelController.Instance.CurrentBoss)
                LevelController.Instance.CurrentBoss.Damage(10);
        }

        if (Input.GetKeyDown(KeyCode.A))
            LevelController.Instance.CurrentBoss = boss;
    }
}
