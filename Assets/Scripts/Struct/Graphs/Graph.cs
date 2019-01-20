using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Graph{

	public OList<Vertex> vertexes;

    public LocationModule locationModule;
    public InfoModule info;
    public ConstructModule construct;
    public ConsistencyModule consistency;
    public PaintModule brush;
	
	
    public MarkModule markModule;
    public CycleModule cycleModule;

	public Graph(GameObject _vertexPrefab, GameObject _edgePrefab, Material _originalMaterial, Material _markedMaterial){
		vertexes = new OList<Vertex> ();
        locationModule = new LocationModule(this);
        info = new InfoModule(this);
        brush = new PaintModule (_originalMaterial, _markedMaterial);
        construct = new ConstructModule(_vertexPrefab, _edgePrefab, brush, this);
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
    /// <param name="_number"></param>
    /// <returns></returns>
    public Vertex GetVertex(int _number){
        return vertexes[_number];
    }

	



    public List<Vertex> GetConnectedVertexes(string _startedVertexName)
    {
        int vertexNumber = vertexes.IndexOf(new Vertex(_startedVertexName));
        List<Vertex> graph = consistency.GetConnectedVertexes(vertexNumber);
        return graph;
    }
		

	public string WriteVertexes(){
		string result = String.Join(" ", vertexes.ToList().Select(item => item.ToString()).ToArray());
		return result;
	}

	public void ResetEdges(){
		construct.ResetEdges ();
	}

    public void Reset()
    {
        construct.ResetEdges();
        construct.ResetVertexes();
        vertexes = new OList<Vertex>();
    }

    public void Construct(OList<MathEdgeStruct> edgesCopy)
    {
        for (int i = 0; i < edgesCopy.Count; i++)
        {
            if (!vertexes.Contains(new Vertex(edgesCopy[i].obj1)))
            {
               construct.AddNewVertex(edgesCopy[i].obj1Position, edgesCopy[i].obj1);
            }

            if (!vertexes.Contains(new Vertex(edgesCopy[i].obj2)))
            {
                construct.AddNewVertex(edgesCopy[i].obj2Position, edgesCopy[i].obj2);
            }

            construct.AddEdge(edgesCopy[i].obj1, edgesCopy[i].obj2, edgesCopy[i].edgeCost);
        }
    }

    public void InsertEdges(OList<EdgeStruct> _edges){
		construct.InsertEdges (_edges);
	}

    //TODO : TO locationModule
    public EdgeObject GetEdge(Vertex A, Vertex B)
    {
        OList<EdgeObject> edges = locationModule.GetEdges();
        OList<EdgeObject> edgesX = new OList<EdgeObject>();
        for (int i = 0; i < edges.Count; i++)
        {
            if(edges[i].obj1.vertexData.VertexName == A.VertexName && edges[i].obj2.vertexData.VertexName == B.VertexName)
            {
                edgesX.Add(edges[i]);
            }
        }
        if (edgesX.Count == 0)
        {
            return null;
        }
        else
        {
            for(int i=0; i<edgesX.Count; i++)
            {
                if(edgesX[i].Sign == Operator.MINUS)
                {
                    return edgesX[i];
                }
            }

            for (int i = 0; i < edgesX.Count; i++)
            {
                if (edgesX[i].Sign == Operator.PLUS)
                {
                    return edgesX[i];
                }
            }
        }
        return null;
    }
    //TODO : To locationModule
    public EdgeObject GetEdge(Vertex A, Vertex B, Operator sign)
    {
        OList<EdgeObject> edges = locationModule.GetEdges();
        for (int i = 0; i < edges.Count; i++)
        {
            if (edges[i].obj1.vertexData.VertexName == A.VertexName && edges[i].obj2.vertexData.VertexName == B.VertexName && edges[i].Sign == sign)
            {
                return edges[i];
            }
        }
        return null;
    }

    public bool Color()
    {
        for(int i=0; i<vertexes.Count; i++)
        {
            vertexes[i].color = -1;
        }
        return vertexes[0].CheckColor(0);
        
    }
}
