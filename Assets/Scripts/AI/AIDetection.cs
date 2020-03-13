using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetection : MonoBehaviour
{
    public bool spotted = false;

    public GameObject player;

    public bool moveTowards;
    public PlayerController pc;

    // When taking over the enemy, make sure to change tag of enemy to player
    void OnTriggerEnter2D(Collider2D coll){
        

        if(coll.gameObject == pc.controlledObject){
            player = coll.gameObject;
            spotted = true;
            moveTowards = false;
            StopCoroutine(lostPlayer());
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        
        if(coll.gameObject == player){
            Debug.Log("lo");
            moveTowards = false;
            StartCoroutine(lostPlayer());
        }
    }

    IEnumerator lostPlayer()
    {
        yield return new WaitForSeconds(2);
        spotted = false;
    }
}