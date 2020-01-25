using System;
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
         Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
         Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
         
         //Get the angle between the points
         float angle = GetAngle(positionOnScreen, mouseOnScreen);

         controlledObject.GetComponent<CharacterMotor>().CharactorRotator(new Vector3(0f, 0f, angle));

        if (Input.GetButtonDown("Fire1"))
        {
            controlledObject.transform.GetComponentInChildren<Weapon>().PrimaryFire();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            controlledObject.transform.GetComponentInChildren<Weapon>().SecondaryFire();
        }

        if (Input.GetButtonDown("Reload"))
        {
            controlledObject.transform.GetComponentInChildren<Weapon>().ReloadAll();
        }

        if (Input.GetButton("Ability1"))
        {
            try
            {
                ItemDatabase.Instance.Abilities[stats.PersistentPlayerData.ActiveAbilities[0]].Use();
            }
            catch (Exception e)
            {
                Debug.LogWarning("Ability not found");
            }
        }

        if (Input.GetButton("Ability2"))
        {
            try
            {
                ItemDatabase.Instance.Abilities[stats.PersistentPlayerData.ActiveAbilities[1]].Use();
            }
            catch (Exception e)
            {
                Debug.LogWarning("Ability not found");
            }
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
        float xVect = Input.GetAxisRaw("Horizontal");
        float yVect = Input.GetAxisRaw("Vertical");
        controlledObject.GetComponent<CharacterMotor>().MovementMotor((new Vector2(xVect,yVect).normalized) * characterSpeed);
    }

    float GetAngle(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
