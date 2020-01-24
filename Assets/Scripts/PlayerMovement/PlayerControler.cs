using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : Player
{

    private void Awake()
    {
        //Players local save for starting game object its controling
        lastControlled = controlledObject;
        controlledObject.GetComponent<CharacterMotor>().TakenOver();
    }

    void Update()
    {
        GlobalManager();
        ControlManager();
    }

    void FixedUpdate()
    {
        MovementManager();
    }

    void ControlManager()
    {
         Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
         Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
         
         //Get the angle between the points
         float angle = GetAngle(positionOnScreen, mouseOnScreen);

         controlledObject.GetComponent<CharacterMotor>().CharactorRotator(new Vector3(0f, 0f, angle));

        if (Input.GetButton("Fire1"))
        {
            throw new System.NotImplementedException();
        }

        if (Input.GetButton("Fire2"))
        {
            throw new System.NotImplementedException();
        }

        if (Input.GetButton("Ability1"))
        {
            throw new System.NotImplementedException();
        }

        if (Input.GetButton("Ability2"))
        {
            throw new System.NotImplementedException();
        }
        if (Input.GetButton("CharJump"))
        {
            throw new System.NotImplementedException();
        }
    }

    void GlobalManager()
    {
        //used to keep track of when the character takes control of a new unity and updates the unit's control system.
        if (controlledObject != lastControlled)
        {
            lastControlled.GetComponent<CharacterMotor>().TakenOver();
            controlledObject.GetComponent<CharacterMotor>().TakenOver();
            lastControlled = controlledObject;
        }
    }

    void MovementManager()
    {
        float Xvect = Input.GetAxisRaw("Horizontal");
        float Yvect = Input.GetAxisRaw("Vertical");
        controlledObject.GetComponent<CharacterMotor>().MovementMotor((new Vector2(Xvect,Yvect).normalized) * characterSpeed); ;
    }

    float GetAngle(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
