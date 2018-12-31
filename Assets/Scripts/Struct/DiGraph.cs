using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiGraph : Graph {
	
	public DiGraph(GameObject _vertexPrefab, GameObject _edgePrefab, Material _originalMaterial, Material _markedMaterial) : 
		base(_vertexPrefab, _edgePrefab, _originalMaterial, _markedMaterial){
		locationModule = new LocationDirectionModule ();
        construct = new DiConstructModule(_vertexPrefab, _edgePrefab, brush);
    }

	public Stack<Vertex> FullVertexesStack(){
		return ((LocationDirectionModule)locationModule).FullVertexesStack (this);
	}

    //TODO : Sprawdzać czy wchodzące krawędzie sa wykorzystywane itd
    public OList<Vertex> GetSources()
    {
        OList<Vertex> output = new OList<Vertex>();
        
        for(int i=0; i<vertexes.Count; i++)
        {
            OList<EdgeObject> edgesGreen = GetEdgesFrom(vertexes[i], Operator.PLUS);
            OList<EdgeObject> edgesTo = GetEdgesTo(vertexes[i], Operator.MINUS);
            if (edgesTo.Count == 0 && edgesGreen.Count == 0)
            {
                output.Add(vertexes[i]);
            }
        }

        return output;
    }

    public OList<Vertex> GetEscapes()
    {
        OList<Vertex> output = new OList<Vertex>();
        for (int i = 0; i < vertexes.Count; i++)
        {
            OList<EdgeObject> edges = GetEdgesFrom(vertexes[i], Operator.MINUS);
            OList<EdgeObject> edgesGreen = GetEdgesTo(vertexes[i], Operator.PLUS);
            if (edges.Count == 0 && edgesGreen.Count == 0)
            {
                output.Add(vertexes[i]);
            }
        }

        return output;
    }

    public OList<EdgeObject> GetEdgesFrom(Vertex _fromVertex, Operator sign = Operator.PLUS)
    {
        OList<EdgeObject> edges = GetEdges();
        OList<EdgeObject> myEdges = new OList<EdgeObject>();
        for (int i = 0; i < edges.Count; i++)
        {
            if (edges[i].obj1.vertexData.VertexName == _fromVertex.VertexName && edges[i].Sign == sign)
            {
                myEdges.Add(edges[i]);
            }
        }
        return myEdges;
    }

    public OList<EdgeObject> GetEdgesTo(Vertex _toVertex, Operator sign = Operator.PLUS)
    {
        OList<EdgeObject> edges = GetEdges();
        OList<EdgeObject> myEdges = new OList<EdgeObject>();
        for (int i = 0; i < edges.Count; i++)
        {
            if (edges[i].obj2.vertexData.VertexName == _toVertex.VertexName && edges[i].Sign == sign)
            {
                myEdges.Add(edges[i]);
            }
        }
        return myEdges;
    }

    public OList<EdgeObject> FindEdgesBetweenAreas(Area<Vertex> a1, Area<Vertex> a2)
    {
        OList<EdgeObject> output = new OList<EdgeObject>();
        for (int i=0; i<a2.Size; i++)
        {
            OList<EdgeObject> edges = GetEdgesFrom(a2[i]);
            for(int j=0; j<edges.Count; j++)
            {
                if (a1.Contains(edges[j].obj2.vertexData))
                {
                    output.Add(edges[j]);
                }
            }
        }
        return output;
    }

    public int GetEdgesValue(OList<EdgeObject> edges)
    {
        int output = 0;
        for(int i=0; i<edges.Count; i++)
        {
            output += edges[i].EdgeCost;
        }
        return output;
    }

    public string GetAssociation()
    {
        string output = "";
        OList<EdgeObject> edges = GetEdges();
        for (int i = 0; i < edges.Count; i++)
        {
            if (edges[i].Sign == Operator.PLUS)
            {
                if(edges[i].obj1.vertexData.VertexName == "T" || edges[i].obj1.vertexData.VertexName == "S"
                    || edges[i].obj2.vertexData.VertexName == "T" || edges[i].obj2.vertexData.VertexName == "S")
                {

                }
                else
                {
                    output += edges[i].ToString() + " ";
                }
                
            }
        }
        return output;
    }
    
}
