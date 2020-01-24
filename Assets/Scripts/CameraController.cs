using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// First version created by: Daniel Bickerdike
// Super simple Camera using Vector3's Lerp() method, could include clamps later on...

public class CameraController : MonoBehaviour
{
    public GameObject PlayerMotor;
    public float speed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(PlayerMotor.transform.position.x, PlayerMotor.transform.position.y, -10), Time.deltaTime * speed);
    }
}
