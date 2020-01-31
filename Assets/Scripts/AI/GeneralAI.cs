using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAI : MonoBehaviour
{

    // If Bradley is reading this: I will route to the CharacterMotor after I have finished each method :)

    // KNOWN BUGS: KEEPS GENERATING 0,0 (UNSURE OF FIX)

    public CharacterMotor motor;
    public Vector2 moveOffset;
    float speed = 1;

    Vector2 originalPosition;
    public Vector2 newPos;

    Vector3 lastFrame;
    bool atOriginalPosition = true;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        newPos = pivotPoint();
    }

    int createMathOperation()
    {
        // 0 = -
        // 1 = +
        int operation = Random.Range(0, 2);

        return operation;
    }

    Vector2 createPivotPoint(int operation, bool _x, bool _y)
    {
        float[] newVectors = { 0,0 };

        if(operation == 0)
        {
            if (_x) { newVectors[0] = transform.position.x - moveOffset.x; }
            if (_y) { newVectors[1] = transform.position.y - moveOffset.y; }
        }

        if (operation == 1)
        {
            if (_x) { newVectors[0] = transform.position.x + moveOffset.x; }
            if (_y) { newVectors[1] = transform.position.y + moveOffset.y; }
        }

        Debug.Log(newVectors[0] + "." + newVectors[1]);
        return new Vector2(newVectors[0], newVectors[1]);
    }

    Vector2 pivotPoint()
    {
        // 0 = x true, y false
        // 1 = x true, y true
        // 2 = x false, y true
        // 3 = x false, y false
        int pivotPosition = Random.Range(0,4);

        Vector2 pivotPoint = new Vector2();

        int operation = createMathOperation();


        if (pivotPosition == 0) { pivotPoint = createPivotPoint(operation, true, false);  }
        if (pivotPosition == 1) { pivotPoint = createPivotPoint(operation, true, true);   }
        if (pivotPosition == 2) { pivotPoint = createPivotPoint(operation, false, true);  }
        if (pivotPosition == 3) { pivotPoint = createPivotPoint(operation, false, false); }

        return pivotPoint;
    }

    // Update is called once per frame
    void Update()
    {
        Idle();
    }

    void Idle()
    {
        Vector2 target = new Vector2(0, 0);

        if (atOriginalPosition)
        {
            target = newPos;
        }
        else
        {
            target = originalPosition;
        }

        transform.position = Vector2.Lerp(transform.position, target, Time.deltaTime * speed);

        if(transform.position == lastFrame)
        {
            if (atOriginalPosition) { atOriginalPosition = false; }
            else { atOriginalPosition = true; }
            newPos = pivotPoint();
        }

        lastFrame = transform.position;
    }

    void Spotted()
    {

    }

    void Shoot()
    {

    }

    void Move()
    {

    }

}
