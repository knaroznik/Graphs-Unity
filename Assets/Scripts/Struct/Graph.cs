using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Graph{

	public OList<Vertex> vertexes;

	protected PaintModule brush;
	protected InfoModule info;
	protected ConstructModule construct;
	protected LocationModule locationModule;

	protected NucleusModule nucleus = new NucleusModule();
	protected ConsistencyModule consistency = new ConsistencyModule();
    public MarkModule markModule;
    public CycleModule cycleModule;

	public Graph(GameObject _vertexPrefab, GameObject _edgePrefab, Material _originalMaterial, Material _markedMaterial){
		vertexes = new OList<Vertex> ();
		locationModule = new LocationModule ();
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

	

	

	protected bool findCycle(int current, List<int> visited, int parent, ref int cyclesFound)
	{
		visited.Add(current);
		for (int i = 0; i < vertexes[current].Count; i++)
		{
			if (vertexes[current][i] == 1)
			{
				if (!visited.Contains(i))
				{
					findCycle(i, visited, current, ref cyclesFound);
				}
				else
				{
					if(parent < 0)
						continue;

					if (i != parent && vertexes[i][parent] == 1)
					{
						cyclesFound++;
						return true;
					}
				}
			}
		}

		return false;
	}

	
	#region Zadanie 2
	public string CheckCyclesOfLength(int x){
		string output = "";
		output += Print ();
		output += "\n\n FINDING CYCLE OF MIN(deg(matrix) + 1 \n\n ";
		output += "\nStarting from C" + x;
		for (int i = x; i <= vertexes.Count; i++) {
			OList<Vertex> cycle = naiveCycles (i);
			if (cycle != null) {
				string result = String.Join("->", cycle.ToList().Select(item => item.ToString()).ToArray());
				output += "\nFound cycle C"+ i +" :\n";
				output += result;
				brush.Paint (cycle);
				break;
			}
		}
		return output;
	}

	protected OList<Vertex> naiveCycles(int x){
		for (int i = 0; i < vertexes.Count; i++) {
			OList<Vertex> q = findCycleLength(i, new OList<Vertex>(), i, x);
			if (q != null) {
				return q;
			}
		}
		return null;
	}

	protected OList<Vertex> findCycleLength(int current, OList<Vertex> visited, int original, int lenght)
	{
		if (visited.Count == lenght-1) {
			if (vertexes [original] [current] == 1) {
				visited.Add (vertexes[current]);
				return visited;
			} else {
				return null;
			}
		} else {
			visited.Add (vertexes[current]);
			for (int i = 0; i < vertexes.Count; i++) {
				if (vertexes [current] [i] == 1) {
					if (!visited.ToList().Contains (vertexes [i])) {
						OList<Vertex> x = new OList<Vertex> (visited.ToList ());
						OList<Vertex> q = findCycleLength (i, x, original, lenght);
						if (q != null) {
							return q;
						}

					}
				}
			}
		}

		return null;
	}
	#endregion

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

	public bool HasCycle(){
		OList<Vertex> cycle = null;
		for (int cycleLength = 3; cycleLength <= vertexes.Count; cycleLength++) {
			for (int i = 0; i < vertexes.Count; i++) {
				OList<Vertex> q = findCycleLength (i, new OList<Vertex> (), i, cycleLength);
				if (q != null) {
					cycle = q;
					break;
				}
			}
		}

		if (cycle != null) {
			return true;
		}

		return false;
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

	public OList<EdgeObject> GetEdges(){
		return locationModule.GetEdges (vertexes);
	}

    public EdgeObject GetEdge(Vertex A, Vertex B)
    {
        OList<EdgeObject> edges = GetEdges();
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

    public EdgeObject GetEdge(Vertex A, Vertex B, Operator sign)
    {
        OList<EdgeObject> edges = GetEdges();
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

    public OList<Vertex> FindBorderers(Vertex _currentVertex)
    {
        return locationModule.FindBorderers(_currentVertex, this);
    }

    public string FindNucleus(){
		return nucleus.FindNucleus (this);
	}

	public OList<EdgeStruct> DFSAlgorithm(){
		return locationModule.DFS (this);
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
