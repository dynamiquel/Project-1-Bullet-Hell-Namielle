using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Transform objectToFollow;
    [SerializeField] BillBoardObjectMode objectMode = BillBoardObjectMode.MainCamera;

    private void Start()
    {
        if (objectMode == BillBoardObjectMode.MainCamera)
            objectToFollow = Camera.main.transform;
    }

    private void LateUpdate()
    {
        try
        {
            transform.LookAt(transform.position + objectToFollow.forward);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Could not rotate object ({name}). Perhaps the specified object does not exist?");
        }
    }
}

public enum BillBoardObjectMode
{
    SpecifiedObject,
    MainCamera
}