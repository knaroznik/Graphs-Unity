using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraStruct {

    public string VertexName;
    public float PathCost;
    public OList<string> Path;

    public DijkstraStruct(string _name)
    {
        VertexName = _name;
        PathCost = Mathf.Infinity;
        Path = new OList<string>();
    }

    public override string ToString()
    {
        return "Wierzchołek " + VertexName + " długość : " + PathCost.ToString() + " a ścieżka to : " + Path.ToString() + "\n";
    }
}
