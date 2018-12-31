using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathEdgeStruct {

    public string obj1;
    public string obj2;
    public Vector3 obj1Position;
    public Vector3 obj2Position;
    public int edgeCost;

    public MathEdgeStruct(string _obj1, Vector3 _obj1Pos, string _obj2, Vector3 _obj2Pos, int _edgeCost)
    {
        obj1 = _obj1;
        obj2 = _obj2;
        obj1Position = _obj1Pos;
        obj2Position = _obj2Pos;
        edgeCost = _edgeCost;
    }
}
