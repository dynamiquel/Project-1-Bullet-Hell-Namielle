using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ask Liam for help.
// Sets a new objective when triggered by the player.
[RequireComponent(typeof(BoxCollider2D))]
public class ObjectiveTrigger : MonoBehaviour
{
    [SerializeField] ObjectiveState objectiveState = ObjectiveState.New;
    [SerializeField] string objectiveDescription = "N/A";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            LevelController.Instance.Objective = new Objective(objectiveState, objectiveDescription);
            Destroy(gameObject);
        }
    }
}
