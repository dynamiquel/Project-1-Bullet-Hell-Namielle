using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetection : MonoBehaviour
{
    public bool spotted = false;

    public GameObject player;

    // When taking over the enemy, make sure to change tag of enemy to player
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag == "Player"){
            player = coll.gameObject;
            spotted = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.gameObject.tag == "Player"){
            //spotted = false;
        }
    }
}