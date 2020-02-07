using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetection : MonoBehaviour
{
    public bool spotted = false;

    public GameObject player;

    public bool moveTowards;

    // When taking over the enemy, make sure to change tag of enemy to player
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag == "Player"){
            player = coll.gameObject;
            spotted = true;
            moveTowards = false;
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.gameObject.tag == "Player"){
            moveTowards = true;
        }
    }

    IEnumerator lostPlayer()
    {
        yield return new WaitForSeconds(2);
        spotted = false;
    }
}