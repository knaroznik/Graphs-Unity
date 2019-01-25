using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Graph{

	public OList<Vertex> vertexes;
    public ConstructModule construct;

    public LocationModule locationModule;
    public InfoModule info;
    public ConsistencyModule consistency;
    public PaintModule brush;
    public MarkModule markModule;
    public CycleModule cycleModule;

	public Graph(GameObject _vertexPrefab, GameObject _edgePrefab, Material _originalMaterial, Material _markedMaterial){
		vertexes = new OList<Vertex> ();
        locationModule = new LocationModule(this);
        info = new InfoModule(this);
        brush = new PaintModule (_originalMaterial, _markedMaterial);
        construct = new ConstructModule(_vertexPrefab, _edgePrefab, brush, this, true);
        consistency = new ConsistencyModule(this, brush);

        markModule = new MarkModule(this); ;
        cycleModule = new CycleModule(this);
    }

    /// <summary>
    /// Size of graph. Count of vertexes.
    /// </summary>
	public int Size{
		get{
			return vertexes.Count;
		}
	}

    /// <summary>
    /// Check if graph is empty.
    /// </summary>
    /// <returns>Is Graph Empty</returns>
    public bool IsEmpty()
    {
        if (Size < 1)
            return true;
        return false;
    }

    /// <summary>
    /// Is Graph Consistent.
    /// </summary>
    /// <returns> True if is consistent, false otherwise.</returns>
    public bool IsConsistent()
    {
        return consistency.IsConsistent();
    }

    /// <summary>
    /// Getting vertex by index.
    /// </summary>
    /// <param name="_number">Index of Vertex.</param>
    /// <returns></returns>
    public Vertex GetVertex(int _number){
        return vertexes[_number];
    }

    /// <summary>
    /// Getting index of vertex by vertexName.
    /// </summary>
    /// <param name="_name">Name of Vertex</param>
    /// <returns></returns>
	public int GetVertexIndex(string _name)
    {
        return vertexes.IndexOf(new Vertex(_name));
    }

    public override string ToString()
    {
        string result = String.Join(" ", vertexes.ToList().Select(item => item.ToString()).ToArray());
        return result;
    }
}
