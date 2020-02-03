using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Pivot))]
public class GeneralAI : MonoBehaviour
{

    // If Bradley is reading this: I will route to the CharacterMotor after I have finished each method :)

    // KNOWN BUGS: KEEPS GENERATING 0,0 (UNSURE OF FIX)
    // DOESNT REVERT BACK TO IDOL (AIDETECTION CLASS)


    public CharacterMotor motor;
    public Pivot _pivot;
    
    float speed = 1;

    public AIDetection fieldDetection;

    Vector2 originalPosition;
    public Vector2 newPos;

    Vector3 lastFrame;
    bool atOriginalPosition = true;

    // Start is called before the first frame update
    void Awake()
    {
        originalPosition = transform.position;
        newPos = _pivot.pivotPoint();
    }

    // Update is called once per frame
    void Update()
    {
        StateController();
    }

    void StateController(){
        if(fieldDetection.spotted){
            Shoot();
        } else {
            Idle();
        }
    }

    void LookAt_Z(Vector3 _target){
        // Get Angle in Radians
             float AngleRad = Mathf.Atan2(_target.y - transform.position.y, _target.x - transform.position.x);
             // Get Angle in Degrees
             float AngleDeg = (180 / Mathf.PI) * AngleRad;
             // Rotate Object
             Quaternion newRotation = Quaternion.Euler(0, 0, AngleDeg);
             this.transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * speed);
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

        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
        LookAt_Z(target);

        if(transform.position == lastFrame)
        {
            if (atOriginalPosition) { atOriginalPosition = false; }
            else { atOriginalPosition = true; }
            newPos = _pivot.pivotPoint();
        }
    }

    void Shoot()
    {
        LookAt_Z(fieldDetection.player.transform.position);
    }
}