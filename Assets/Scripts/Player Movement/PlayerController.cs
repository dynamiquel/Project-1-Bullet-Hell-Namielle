using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
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
         Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(controlledObject.transform.position);
         float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;

        
         controlledObject.GetComponent<CharacterMotor>().CharactorRotator(angle);

        if (Input.GetButtonDown("Fire1"))
        {
            print("Ping");
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
        this.transform.position = controlledObject.transform.position;
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
        float yVect = Input.GetAxisRaw("Horizontal");
        float xVect = Input.GetAxisRaw("Vertical");
        controlledObject.GetComponent<CharacterMotor>().MovementMotor((new Vector2(xVect,yVect).normalized) * characterSpeed);
    }
}
