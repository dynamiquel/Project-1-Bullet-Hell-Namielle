using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    public Vector2 moveOffset;

    int createMathOperation()
    {
        // 0 = -
        // 1 = +
        int operation = Random.Range(0, 2);

        return operation;
    }

    public Vector3 createPivotPoint(int operation, bool _x, bool _y)
    {
        float[] newVectors = { 0,0 };
        float randomPositionX = Random.Range(0, moveOffset.x);
        float randomPositionY = Random.Range(0, moveOffset.y);


        if(operation == 0)
        {
            if (_x) { newVectors[0] = transform.position.x - randomPositionX; }
            if (_y) { newVectors[1] = transform.position.y - randomPositionY; }
        }

        if (operation == 1)
        {
            if (_x) { newVectors[0] = transform.position.x + randomPositionX; }
            if (_y) { newVectors[1] = transform.position.y + randomPositionY; }
        }

        Debug.Log(newVectors[0] + "." + newVectors[1]);
        return new Vector3(newVectors[0], newVectors[1], 1);
    }

    public Vector3 pivotPoint()
    {
        // 0 = x true, y false
        // 1 = x true, y true
        // 2 = x false, y true
        // 3 = x false, y false
        int pivotPosition = Random.Range(0,4);

        Vector3 pivotPoint = new Vector3();

        int operation = createMathOperation();


        if (pivotPosition == 0) { pivotPoint = createPivotPoint(operation, true, false);  }
        if (pivotPosition == 1) { pivotPoint = createPivotPoint(operation, true, true);   }
        if (pivotPosition == 2) { pivotPoint = createPivotPoint(operation, false, true);  }
        if (pivotPosition == 3) { pivotPoint = createPivotPoint(operation, false, false); }

        return pivotPoint;
    }
}
