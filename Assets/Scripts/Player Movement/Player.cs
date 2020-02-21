using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float characterSpeed;
    public GameObject controlledObject;
    public GameObject lastControlled;
    public PlayerStats stats;

    public virtual void Start()
    {
       stats = new PlayerStats(SaveManager.Instance.Load());
    }
}
