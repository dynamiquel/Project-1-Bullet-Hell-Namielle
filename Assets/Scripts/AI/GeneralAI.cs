using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Pivot))]
public class GeneralAI : MonoBehaviour
{

    // AI SCRIPT AWAITING SHOOTING SCRIPT, NOT FINISHED BUT HALTING PROGRESS FOR NOW.


    public CharacterMotor motor;
    public Pivot _pivot;
    
    public float pivotSpeed = 1;
    public float followSpeed = 1;

    public AIDetection fieldDetection;

    Vector2 originalPosition;
    public Vector2 newPos;

    Vector3 lastFrame;
    bool atOriginalPosition = true;

    [Range(1.0f, 10.0f)]
    public float erratic;

    public PlayerController _pc;
    GameObject player;

    public Weapon weapon;
    public float shootSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        originalPosition = transform.position;
        StartCoroutine(triggerNewPosition());
    }

    // Update is called once per frame
    void Update()
    {
        StateController();
        if (fieldDetection.moveTowards) { transform.position = Vector3.MoveTowards(transform.position, fieldDetection.player.transform.position, followSpeed * Time.deltaTime); }
    }

    void StateController(){
        if(fieldDetection.spotted){
            Shoot();
        } else {
            Idle();
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

    void Idle()
    {

        Vector3 target = new Vector3(0, 0, 0);

        if (atOriginalPosition)
        {
            target = newPos;
        }
        else
        {
            target = originalPosition;
        }

        transform.position = Vector3.Lerp(transform.position, target , Time.deltaTime * pivotSpeed);
        LookAt_Z(target, false);
    }

    void Shoot()
    {
        LookAt_Z(fieldDetection.player.transform.position, true);
        StartCoroutine(ShootWeapon());


    }

    IEnumerator triggerNewPosition()
    {
        while (1 == 1)
        {
            Vector2 _tempPivot = _pivot.pivotPoint();
            newPos = _tempPivot;
            if (atOriginalPosition) { atOriginalPosition = false; }
            else { atOriginalPosition = true; }
            yield return new WaitForSeconds(erratic);
        }
    }

    IEnumerator ShootWeapon()
    {
        weapon.ReloadAll();
        weapon.PrimaryFire();
        yield return new WaitForSeconds(shootSpeed);
    }
}