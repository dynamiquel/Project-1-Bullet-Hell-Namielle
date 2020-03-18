using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMotor : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool isControlled = false;
    public bool previouslyControlled = false;

    public void TakenOver()
    {
        isControlled = !isControlled;

        // If the motor was just controlled but not anymore.
        if (!isControlled)
        {
            previouslyControlled = true;
            StartCoroutine(AIDelay());
        }

        rb.mass = isControlled ? 1 : 1000;
    }

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void MovementMotor(Vector2 movementvect)
    {
        gameObject.transform.Translate(movementvect * Time.fixedDeltaTime);
    }

    // Like a twin-stick game. Up = up, not towards look. I prefer it and I think it's required for controller.
    // We can add this as in an option in the options menu for people who prefer the other one.
    public void MovementMotor_Compass(Vector2 moveVec)
    {
        rb.MovePosition(moveVec);
    }

    public void CharactorRotator(float rotationAngle)
    {
        rb.rotation = rotationAngle;
    }

    // Prevents the AI from instantly attacking the player after the player has left the enemy. It's annoying.
    IEnumerator AIDelay()
    {
        var AI = GetComponent<GeneralAI>();

        AI.enabled = false;
        yield return new WaitForSeconds(0.5f);
        AI.enabled = true;
    }
}
