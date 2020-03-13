using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    public bool CanJump { get => currJumpCooldown <= 0f; }
    [SerializeField] float jumpDistance = 7f;

    GameObject controlledEnemy = null;
    GameObject potentialEnemy = null;

    Transform controlledEnemyTransform = null;

    [SerializeField] float jumpCooldown = 2f;
    float currJumpCooldown = float.Epsilon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currJumpCooldown > float.Epsilon)
            currJumpCooldown -= Time.deltaTime;

        // If the player can jump.
        if (currJumpCooldown <= float.Epsilon)
            FireRaycast();
    }

    public void SetCurrentControlledEnemy(GameObject newEnemy)
    {
        controlledEnemy = newEnemy;
        controlledEnemyTransform = controlledEnemy.transform;
        currJumpCooldown = jumpCooldown;
    }

    void FireRaycast()
    {
        // Can't figure out how to cast it, x number of metres away from player (it keeps hitting itself).
        RaycastHit2D rayCastHit = Physics2D.Raycast(new Vector3(controlledEnemyTransform.position.x + 3f, controlledEnemyTransform.position.y + 3f).ToVector2(), controlledEnemyTransform.TransformDirection(Vector2.up), jumpDistance);
        print("Fired");

        if (rayCastHit.collider != null)
        {
            Debug.DrawRay(controlledEnemyTransform.position, controlledEnemyTransform.TransformDirection(Vector2.up) * jumpDistance, Color.yellow, 1f);
            CharacterMotor characterMotor = rayCastHit.collider.GetComponent<CharacterMotor>();

            if (characterMotor != null)
            {
                print("Enemy raycast");
                rayCastHit.collider.name = "ME"; 
            }
        }
    }
}
