using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{
    public event Action<Weapon> OnWeaponChanged;

    Weapon currentWeapon;
    CharacterMotor currentCharacterMotor;

    private void Awake()
    {
        //Players local save for starting game object its controling
        HandleNewControlledObject();

        if (LevelController.Instance)
            LevelController.Instance.PlayerController = this;
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

    void HandleNewControlledObject()
    {
        try
        {
            currentCharacterMotor = controlledObject.GetComponent<CharacterMotor>();
            currentCharacterMotor.TakenOver();
        }
        catch (Exception e)
        {
            Debug.LogWarning("No character motor found on controlled object.");
        }

        lastControlled = controlledObject;

        try
        {
            currentWeapon = controlledObject.GetComponentInChildren<Weapon>();
            OnWeaponChanged?.Invoke(currentWeapon);
        }
        catch (Exception e)
        {
            Debug.LogWarning("No weapon found on controlled object.");
        }
    }

    void ControlManager()
    {
         Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(controlledObject.transform.position);
         float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;

        if (currentCharacterMotor)
            currentCharacterMotor.CharactorRotator(angle);

        if (Input.GetButtonDown("Fire1"))
        {
            print("Ping");
            if (currentWeapon)
            {
                currentWeapon.PrimaryFire();
                OnWeaponChanged?.Invoke(currentWeapon);
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (currentWeapon)
            {
                currentWeapon.SecondaryFire();
                OnWeaponChanged?.Invoke(currentWeapon);
            }
        }

        if (Input.GetButtonDown("Reload"))
        {
            if (currentWeapon)
            {
                currentWeapon.ReloadAll();
                OnWeaponChanged?.Invoke(currentWeapon);
            }
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
            HandleNewControlledObject();
        }
    }

    void MovementManager()
    {
        float yVect = Input.GetAxisRaw("Horizontal");
        float xVect = Input.GetAxisRaw("Vertical");
        currentCharacterMotor.MovementMotor((new Vector2(xVect,yVect).normalized) * characterSpeed);
    }
}
