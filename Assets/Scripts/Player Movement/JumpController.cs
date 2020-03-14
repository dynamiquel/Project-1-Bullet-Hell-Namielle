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
    Transform raycastTransform;

    [SerializeField] float jumpCooldown = 2f;
    float currJumpCooldown = float.Epsilon;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currJumpCooldown > float.Epsilon)
            currJumpCooldown -= Time.deltaTime;

        // If the player can jump.
        if (currJumpCooldown <= float.Epsilon)
            FireRaycast();
        else
            potentialEnemy = null;
    }

    public void SetCurrentControlledEnemy(GameObject newEnemy)
    {
        controlledEnemy = newEnemy;
        controlledEnemyTransform = controlledEnemy.transform;
        currJumpCooldown = jumpCooldown;
        raycastTransform = controlledEnemyTransform.Find("Raycaster");
    }

    public bool Jump(out GameObject newEnemy)
    {
        newEnemy = potentialEnemy;
        if (potentialEnemy == null)
            return false;
        
        currJumpCooldown = jumpCooldown;
        return true;
    }

    void FireRaycast()
    {
        // Can't figure out how to cast it, x number of metres away from player (it keeps hitting itself).
        RaycastHit2D rayCastHit = Physics2D.Raycast(raycastTransform.position, controlledEnemyTransform.TransformDirection(Vector2.up), jumpDistance);

        if (rayCastHit.collider != null)
        {
            Debug.DrawRay(raycastTransform.position, controlledEnemyTransform.TransformDirection(Vector2.up) * jumpDistance, Color.yellow, 1f);
            
            CharacterMotor characterMotor = rayCastHit.collider.GetComponent<CharacterMotor>();

            // Just in case the ray cast  hits a child collider. (Currently, this includes the 'Field' collider
            // for the AI).
            if (characterMotor == null)
                characterMotor = rayCastHit.collider.transform.GetComponentInParent<CharacterMotor>();
            
            if (characterMotor != null)
            {
                print("Ready to jump");
                potentialEnemy = characterMotor.gameObject;
            }
            else
            {
                potentialEnemy = null;
            }
        }
    }
}
