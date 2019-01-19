using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Graph{

	public OList<Vertex> vertexes;

    public LocationModule locationModule;


    public PaintModule brush;
	protected InfoModule info;
	protected ConstructModule construct;
	
    
	protected ConsistencyModule consistency = new ConsistencyModule();
    public MarkModule markModule;
    public CycleModule cycleModule;

	public Graph(GameObject _vertexPrefab, GameObject _edgePrefab, Material _originalMaterial, Material _markedMaterial){
		vertexes = new OList<Vertex> ();
		locationModule = new LocationModule (this);
		info = new InfoModule (this);
		brush = new PaintModule (_originalMaterial, _markedMaterial);
		construct = new ConstructModule (_vertexPrefab, _edgePrefab,brush);
        markModule = new MarkModule(this); ;
        cycleModule = new CycleModule(this);
    }

	public int Count{
		get{
			return vertexes.Count;
		}
	}

    public bool IsEmpty()
    {
        if (Count < 1)
            return true;
        return false;
    }

	public int LowestValue(){
		return info.LowestValue ();
	}

	#region Construct Module 

	public void AddVertex(string _newVertexName){
		construct.AddVertex (_newVertexName, ref vertexes);
	}

    public void AddNewVertex(Vector3 vertexPosition, string _vertexName = "")
    {
        construct.AddNewVertex(vertexPosition, ref vertexes, _vertexName);
    }

    public void RemoveVertex(string _vertexName){
		construct.RemoveVertex (_vertexName, ref vertexes);
	}

	public void AddEdge(string one, string two, string edgeCost){
		construct.AddEdge (one, two, edgeCost, ref vertexes);
	}

	public void AddEdge(string one, string two, int edgeCost, Operator sign = Operator.MINUS){
		construct.AddEdge (one, two, edgeCost, ref vertexes, sign);
	}

	public void RemoveEdge(string one, string two){
		construct.RemoveEdge (one, two, ref vertexes);
	}

	#endregion

	public string Print(){
		string output = "";
		output += " Macierz sąsiedztwa : \n";
		for (int i = 0; i < vertexes.Count; i++)
		{
			output += "\t" + vertexes[i].VertexName;
		}

		for (int i = 0; i < vertexes.Count; i++)
		{
			output += "\n";
			for (int j = 0; j < vertexes.Get(i).Count; j++)
			{
				if(j == 0)
				{
					output += vertexes[i].VertexName;
				}

				output += "\t" + vertexes[i][j];                  
			}
		}
		output += info.PrintInfo ();
		return output;
	}


	public bool IsConsistent(){
		return consistency.IsConsistent (vertexes);
	}

    public List<Vertex> GetConnectedVertexes(string _startedVertexName)
    {
        int vertexNumber = vertexes.IndexOf(new Vertex(_startedVertexName));
        List<Vertex> graph = consistency.GetConnectedVertexes(vertexes, vertexNumber);
        return graph;
    }
		

	public string WriteVertexes(){
		string result = String.Join(" ", vertexes.ToList().Select(item => item.ToString()).ToArray());
		return result;
	}

	public void ResetEdges(){
		construct.ResetEdges (this);
	}

    public void Reset()
    {
        construct.ResetEdges(this);
        construct.ResetVertexes(this);
        vertexes = new OList<Vertex>();
    }

    public void Construct(OList<MathEdgeStruct> edgesCopy)
    {
        for (int i = 0; i < edgesCopy.Count; i++)
        {
            if (!vertexes.Contains(new Vertex(edgesCopy[i].obj1)))
            {
                AddNewVertex(edgesCopy[i].obj1Position, edgesCopy[i].obj1);
            }

            if (!vertexes.Contains(new Vertex(edgesCopy[i].obj2)))
            {
                AddNewVertex(edgesCopy[i].obj2Position, edgesCopy[i].obj2);
            }

            AddEdge(edgesCopy[i].obj1, edgesCopy[i].obj2, edgesCopy[i].edgeCost);
        }
    }

    public void InsertEdges(OList<EdgeStruct> _edges){
		construct.InsertEdges (_edges, this);
	}

	public void PaintConsistency(){
		consistency.PaintConsistency (vertexes, brush);
	}

	public void PaintConsistency(OList<OList<Vertex>> consistencyParts){
		brush.Paint (consistencyParts);
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

    public Vertex GetVertexByName(string _name)
    {
        for(int i=0; i<vertexes.Count; i++)
        {
            if(vertexes[i].VertexName == _name)
            {
                return vertexes[i];
            }
        }
        return null;
    }

    public Vertex GetVertex(int _number)
    {
        return vertexes[_number];
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
