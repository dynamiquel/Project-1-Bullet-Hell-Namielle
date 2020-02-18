using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveSection : HUDComponent
{
    [SerializeField]
    TextMeshProUGUI objectiveStateText;
    [SerializeField]
    TextMeshProUGUI objectiveInfoText;

    // Start is called before the first frame update
    void Start()
    {
        LevelController.Instance.OnObjectiveChanged += HandleObjectiveChanged;
        HandleObjectiveChanged(LevelController.Instance.Objective);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleObjectiveChanged(Objective objective)
    {
        string state = string.Empty;

        switch (objective.state)
        {
            case ObjectiveState.New:
                state = "New Objective";
                break;
            case ObjectiveState.Completed:
                state = "Objective Complete";
                break;
            case ObjectiveState.Updated:
                state = "Objective Updated";
                break;
        }

        StartCoroutine(ShowObjective(objective.description, state));
    }

    IEnumerator ShowObjective(string description, string state)
    {
        objectiveInfoText.text = description;
        objectiveStateText.text = state;

        yield return new WaitForSeconds(5);

        objectiveInfoText.text = string.Empty;
        objectiveStateText.text = string.Empty;
    }
}
