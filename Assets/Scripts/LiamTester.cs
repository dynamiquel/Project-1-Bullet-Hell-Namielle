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
}
