using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : Player
{

    void Update()
    {
        GlobalManager();
        ControlerManger();
    }

    void GlobalManager()
    {

        if (controlledObject != lastControlled)
        {
            controlledObject.GetComponent<CharacterMotor>().TakenOver();
        }

    }

    void ControlerManger()
    {
        float Xvect = Input.GetAxisRaw("Horizontal");
        float Yvect = Input.GetAxisRaw("Vertical");
        controlledObject.GetComponent<CharacterMotor>().MovementMotor((new Vector2(Xvect,Yvect)) * characterSpeed); ;
    }
}
