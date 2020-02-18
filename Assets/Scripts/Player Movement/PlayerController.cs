using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{
    public event Action<Weapon> OnWeaponChanged;
    public event Action OnTakeover;
    public event Action<IDamageable> OnHealthChange;

    Weapon currentWeapon;
    CharacterMotor currentCharacterMotor;

    private void Awake()
    {
        // Players local save for starting game object its controlling.
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

    public Weapon GetWeapon()
    {
        return currentWeapon;
    }

    public IDamageable GetControlledIDamagable()
    {
        return controlledObject.GetComponentInChildren<IDamageable>();
    }

    void HandleHealthChange(IDamageable entity)
    {
        if (entity.Health <= 0)
        {
            Debug.Log("You died. We haven't done anything past this point ;)");
            // Unsubscribe so runtime errors don't happen.
            GetControlledIDamagable().OnHealthChanged -= HandleHealthChange;
        }

        OnHealthChange?.Invoke(entity);
    }

    void HandleNewControlledObject()
    {
        // So many try-catches ;)
        try
        {
            lastControlled.GetComponentInChildren<IDamageable>().OnHealthChanged -= OnHealthChange;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Could not unsubscribe OnHealthChange from last controlled object. Ignore if this is the first controller.");
        }

        try
        {
            GetControlledIDamagable().OnHealthChanged += HandleHealthChange;
        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning($"Could not subscribe to OnHealthChanged for currently controlled object as {controlledObject.name} does not have an IDamagable interface.");
        }

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

            OnTakeover?.Invoke();
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
