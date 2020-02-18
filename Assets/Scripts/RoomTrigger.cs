using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Automatically disables the given game objects until it is triggered by the player.
[RequireComponent(typeof(BoxCollider2D))]
public class RoomTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> gameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var go in gameObjects)
            go.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            foreach (var go in gameObjects)
                go.SetActive(true);

            Destroy(gameObject);
        }
    }
}
