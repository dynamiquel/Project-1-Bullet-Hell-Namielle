﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ask Bradley or Liam for help.
[RequireComponent(typeof(PerkController))]
[RequireComponent(typeof(JumpController))]
public class PlayerController : Player
{
    public event Action<Weapon> OnWeaponChanged;
    public event Action OnTakeover;
    public event Action<IDamageable> OnHealthChange;
    public event Action<Player> OnPlayerDeath;
    public bool CompassMovement { get; set; } = true;

    Weapon currentWeapon;
    CharacterMotor currentCharacterMotor;
    public PerkController PerkController { get; private set; }
    public AbilityController AbilityController { get; private set; }
    Vector2 mousePos;

    [SerializeField] JumpController jumpController;

    #region Walk Sounds
    [SerializeField] AudioSource feetAudioSource;
    [SerializeField] float walkSoundRate = 1f;
    float currentWalkSoundDelay = 0.5f;
    [SerializeField]
    string[] walkAudioClipsId;
    byte currentWalkAudioIndex = 0;
    #endregion

    private void Awake()
    {
        // Players local save for starting game object its controlling.
        HandleNewControlledObject();

        if (LevelController.Instance)
            LevelController.Instance.PlayerController = this;

        PerkController = GetComponent<PerkController>();
        jumpController = GetComponent<JumpController>();
    }

    public override void Start()
    {
        base.Start();
        StartCoroutine(SetupPerks());
        stats.OnPlayerLevelUp += HandlePlayerLevelUp;
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
        return controlledObject.GetComponent<IDamageable>();
    }

    // Public access to invoke the OnWeaponChanged event.
    public void WeaponChanged()
    {
        OnWeaponChanged?.Invoke(currentWeapon);
    }

    void HandleHealthChange(IDamageable entity)
    {
        if (entity.Health <= 0)
        {
            Debug.Log("You died. We haven't done anything past this point ;)");
            // Unsubscribe so runtime errors don't happen.
            entity.OnHealthChanged -= HandleHealthChange;
            OnPlayerDeath?.Invoke(this);
        }

        OnHealthChange?.Invoke(entity);
    }

    void HandleNewControlledObject()
    {
        // So many try-catches ;)
        try
        {
            lastControlled.GetComponent<IDamageable>().OnHealthChanged -= HandleHealthChange;
            lastControlled.GetComponent<CharacterMotor>().TakenOver();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Could not unsubscribe OnHealthChange from last controlled object. Ignore if this is the first controller.");
        }

        try
        {
            GetControlledIDamagable().OnHealthChanged += HandleHealthChange;
            OnHealthChange?.Invoke(GetControlledIDamagable());
        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning($"Could not subscribe to OnHealthChanged for currently controlled object as {controlledObject.name} does not have an IDamagable interface.");
        }

        try
        {
            currentCharacterMotor = controlledObject.GetComponent<CharacterMotor>();
            AbilityController = controlledObject.GetComponent<AbilityController>();
            currentCharacterMotor.TakenOver();
        }
        catch (Exception e)
        {
            Debug.LogWarning("No character motor or ability controller found on controlled object.");
        }

        lastControlled = controlledObject;

        try
        {
            if (currentWeapon != null)
                currentWeapon.IsPlayer = false;
            
            currentWeapon = controlledObject.GetComponentInChildren<Weapon>();
            currentWeapon.IsPlayer = true;
            OnWeaponChanged?.Invoke(currentWeapon);

            if (!currentCharacterMotor.previouslyControlled && PerkController.CanUse("jugg"))
            {
                var enemyComp = currentCharacterMotor.GetComponent<Enemy>();
                enemyComp.Health = enemyComp.MaxHealth;
                OnHealthChange?.Invoke(GetControlledIDamagable());
            }

            OnTakeover?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogWarning("No weapon found on controlled object.");
        }

        jumpController.SetCurrentControlledEnemy(controlledObject);
    }

    void ControlManager()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetAxis("Fire1") == 1)
        {
            //print("Ping");
            if (currentWeapon)
            {
                // Attempt to fire the weapon, if the weapon does fire, increase bullets shot.
                if (currentWeapon.PrimaryFire())
                    stats.BulletsShot++;

                OnWeaponChanged?.Invoke(currentWeapon);
            }
        }

        if (Input.GetAxis("Fire2") == 1)
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

        if (Input.GetButtonDown("Ability1"))
        {
            try
            {
                if (!AbilityController)
                    return;
                
                AbilityController.UseAbility(0);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Ability 1 not found");
            }
        }

        if (Input.GetButtonDown("Ability2"))
        {
            try
            {
                AbilityController.UseAbility(1);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Ability 2 not found");
            }
        }
        if (Input.GetButton("CharJump"))
        {
            if (jumpController.Jump(out var newEnemy))
            {
                controlledObject = newEnemy;
                GetComponent<AudioSource>().PlayOneShot(AudioDatabase.GetClip("Body Swap"));
                HandleNewControlledObject();
            }
        }
    }

    void GlobalManager()
    {
        transform.position = controlledObject.transform.position;

        //used to keep track of when the character takes control of a new unity and updates the unit's control system.
        if (controlledObject != lastControlled)
        {
            lastControlled.GetComponent<CharacterMotor>().TakenOver();
            HandleNewControlledObject();
        }
    }

    void MovementManager()
    {
        UpdatePlayerMovement();
        UpdatePlayerRotation();
    }

    void UpdatePlayerMovement()
    {
        float xVect = Input.GetAxisRaw("Horizontal");
        float yVect = Input.GetAxisRaw("Vertical");

        // If no movement...
        if (xVect == 0 && yVect == 0)
            return;

        if (CompassMovement)
        {
            Vector2 controllerPosition = new Vector2(xVect, yVect).normalized;
            Vector2 desiredLocation = currentCharacterMotor.rb.position + (controllerPosition * 0.02f * characterSpeed); // Float is used to slow down the player as it's too fast without it.
            currentCharacterMotor.MovementMotor_Compass(desiredLocation);
        }
        else
            currentCharacterMotor.MovementMotor((new Vector2(xVect, yVect).normalized) * characterSpeed);

        UpdateWalkSound();
    }

    void UpdatePlayerRotation()
    {
        Vector2 lookDir;
        float rotation;

        // If controller.
        if (Input.GetAxis("Horizontal_2") != 0 || Input.GetAxis("Vertical_2") != 0)
            lookDir = new Vector2(-Input.GetAxis("Horizontal_2"), Input.GetAxis("Vertical_2"));
        else // Else if mouse.
            lookDir = mousePos - currentCharacterMotor.rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        currentCharacterMotor.CharactorRotator(angle);
    }

    IEnumerator SetupPerks()
    {
        yield return new WaitUntil(() => ItemDatabase.Instance.Loaded);

        if (PerkController != null)
        {
            foreach (var savedPerk in stats.PersistentPlayerData.UnlockedPerks)
                PerkController.AddPerk(savedPerk);
            Debug.Log("Player's perks have been added.");
        }
        else
            Debug.LogError("No Perk Controller was found.");                
    }

    void UpdateWalkSound()
    {
        if ((currentWalkSoundDelay += Time.deltaTime) >= walkSoundRate)
        {
            currentWalkSoundDelay = 0f;
            feetAudioSource.PlayOneShot(AudioDatabase.GetClip(walkAudioClipsId[currentWalkAudioIndex]));

            if (currentWalkAudioIndex < walkAudioClipsId.Length - 1)
                currentWalkAudioIndex++;
            else
                currentWalkAudioIndex = 0;
        }
    }

    void HandlePlayerLevelUp(int newLevel)
    {
        Debug.Log(string.Format("Player is now level: {0}", newLevel));
        stats.PersistentPlayerData.PerkPoints++;
        // Play level up sound
        GetComponent<AudioSource>().PlayOneShot(AudioDatabase.GetClip("Pickup"));
    }
}
