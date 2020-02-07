
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// First version created by: Daniel Bickerdike
// Super simple Camera using Vector3's Lerp() method, could include clamps later on...

public class CameraController : MonoBehaviour
{
    public GameObject PlayerMotor;
    public float speed = 15f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(PlayerMotor.transform.position.x, PlayerMotor.transform.position.y, -10), Time.deltaTime * speed);
    }
}