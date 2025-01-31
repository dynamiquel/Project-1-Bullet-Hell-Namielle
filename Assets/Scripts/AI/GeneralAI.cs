﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GeneralAI : MonoBehaviour
{

    // AI SCRIPT AWAITING SHOOTING SCRIPT, NOT FINISHED BUT HALTING PROGRESS FOR NOW.
    
    public float pivotSpeed = 1;
    public float followSpeed = 1;

    public AIDetection fieldDetection;

    Vector2 originalPosition;
    public Vector2 newPos;

    Vector3 lastFrame;
    bool atOriginalPosition = true;

    [Range(1.0f, 10.0f)]
    public float erratic;

    PlayerController _pc;
    GameObject player;

    public Weapon weapon;
    public float shootSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        originalPosition = transform.position;
    }

    void Start(){
        _pc = LevelController.Instance.PlayerController;
        weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_pc.controlledObject != this.gameObject){
            fieldDetection.gameObject.SetActive(true);
            StateController();
            if (fieldDetection.moveTowards) { transform.position = Vector3.MoveTowards(transform.position, fieldDetection.player.transform.position, followSpeed * Time.deltaTime); }
        } else {
            fieldDetection.gameObject.SetActive(false);
        }
    }

    void StateController(){
        if(fieldDetection.spotted){
            Shoot();
        }
    }

    void LookAt_Z(Vector3 _target, bool shooting){
        // Get Angle in Radians
             float AngleRad = Mathf.Atan2(_target.y - transform.position.y, _target.x - transform.position.x);
             // Get Angle in Degrees
             float AngleDeg = (180 / Mathf.PI) * AngleRad;
             // Rotate Object
             Quaternion newRotation = Quaternion.Euler(0, 0, AngleDeg - 90);
             float _speed;
             if (shooting){ _speed = followSpeed; } else { _speed = pivotSpeed; }
             this.transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * _speed);
    }

    void Shoot()
    {
        LookAt_Z(fieldDetection.player.transform.position, true);
        StartCoroutine(ShootWeapon());
    }

    IEnumerator ShootWeapon()
    {
        if (weapon == null)
            yield break;
        
        weapon.PrimaryFire();
        
        if (weapon.primaryClipAmmo <= 0)
            weapon.ReloadAll();
        
        yield return new WaitForSeconds(shootSpeed);
    }
    
    // To get ability controller, do .GetComponent<AbilityController>();
    // To add abilities normally, go the inspector and add some abilityIds to the abilitiesIds list.
    // To add abilities at runtime, do abilityController.AddAbility(abilityId, index);
    // To use an ability, do abilityController.Use(index);
    // Keep in mind that the player can only use index 0 and 1, so any abilities at other indexes are AI-exclusive.
}