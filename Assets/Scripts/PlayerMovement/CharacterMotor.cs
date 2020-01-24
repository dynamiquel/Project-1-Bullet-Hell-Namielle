using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMotor : MonoBehaviour
{
    Rigidbody2D rb;
    bool isControlled = false;

    public void TakenOver()
    {
        isControlled = true;
    }

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void MovementMotor(Vector2 movementvect)
    {
        rb.velocity = movementvect;
    }

    public void CharactorRotator(Vector2 rotationVector)
    {
        transform.Rotate(rotationVector);
    }
}
