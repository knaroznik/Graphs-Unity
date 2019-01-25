using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MathVertex {

	private OList<GameObject> connectionObjects;

	private GameObject vertexObject;
	private string vertexName;

	public GameObject VertexObject{
		get{
			return vertexObject;
		}

		set{
			vertexObject = value;
		}
	}

	public string VertexName{
		get{
			return vertexName;
		}
	}

	public override bool Equals (object obj)
	{
		
		if (obj.GetType () == this.GetType ()) {
			Vertex x = (Vertex)obj;
			if (x.VertexName.Equals(this.VertexName)) {
				return true;
			}
		}
		return false;
	}

	public Vertex(GameObject _vertexObject, string _vertexName) : base(){
		connectionObjects = new OList<GameObject> ();
		VertexObject = _vertexObject;
		vertexName = _vertexName;

		VertexObject.GetComponent<VertexObject> ().SetName (_vertexName, this);
	}

	public Vertex(string _vertexName){
		vertexName = _vertexName;
	}

	public Vertex(string _name, OList<int> _connections){
		vertexName = _name;
		connections = new OList<int> ();
		for (int i = 0; i < _connections.Count; i++) {
			connections.Add (_connections [i]);
		}
	}

	public void AddPossibility(GameObject _newVertex){
		base.AddPossibility ();
		connectionObjects.Add (_newVertex);
	}

	public void AddPossibilities(int howMuch, OList<Vertex> _vertexes){
		for (int i = 0; i < howMuch; i++) {
			connections.Add (0);
			connectionObjects.Add (_vertexes [i].VertexObject);
		}
	}

	public new void RemoveAt(int i){
		base.RemoveAt (i);
		if (connectionObjects != null) {
			connectionObjects.RemoveAt (i);
		}
	}

	public override string ToString ()
	{
		return VertexName.ToString ();
	}

    #region Cechowanie

    public new int Value
    {
        get
        {
            return VertexObject.GetComponent<VertexObject>().Value;
        }
    }

    public Vertex pathVertex;
    public float pathCost;
    public Operator sign;

    public void Reset()
    {
        pathVertex = null;
        pathCost = Mathf.Infinity;
    }

    public string Return(Graph _graph, float _pathCost)
    {
        if (pathVertex == null)
        {
            return "(" + VertexName + " " + sign.ToString() + ")";
        }
        //Odjąć koszt ze znanej ścieżki, 
        EdgeObject edge = _graph.locationModule.GetEdge(pathVertex, this, sign);
        edge.EdgeCost -= (int)_pathCost;
        if (edge.EdgeCost == 0)
        {
            _graph.construct.RemoveEdge(pathVertex.vertexName, VertexName);
        }

        //dodać do ścieżki w drugą stronę
        Operator oppositeSign = Operator.PLUS;
        if(sign == Operator.PLUS)
        {
            oppositeSign = Operator.MINUS;
        }
        else
        {
            oppositeSign = Operator.PLUS;
        }

        edge = _graph.locationModule.GetEdge(this, pathVertex, oppositeSign);
        if (edge == null)
        {
            _graph.construct.AddEdge(VertexName, pathVertex.VertexName, 0, oppositeSign);
            edge = _graph.locationModule.GetEdge(this, pathVertex, oppositeSign);
        }

        edge.EdgeCost += (int)_pathCost;
        return "(" + VertexName + ")" + " " + pathVertex.Return(_graph, _pathCost);
    }
    #endregion

    public OList<Vertex> GetConnectedVertexes()
    {
        OList<Vertex> output = new OList<Vertex>();
        for(int i=0; i<connections.Count; i++)
        {
            if(connections[i] == 1)
            {
                output.Add(connectionObjects[i].GetComponent<VertexObject>().vertexData);
            }
        }
        return output;
    }

    public int color;

    public bool CheckColor(int wantedColor)
    {
        //Algorytm jeszcze nie był w tym wierzchołku
        if (color == -1)
        {
            color = wantedColor;
            OList<bool> coloring = new OList<bool>();
            int nextColor = -1;
            if (wantedColor == 0)
            {
                nextColor = 1;
            }
            else
            {
                nextColor = 0;
            }
            OList<Vertex> vertexes = GetConnectedVertexes();
            for(int i=0; i< vertexes.Count; i++)
            {
                coloring.Add(vertexes[i].CheckColor(nextColor));
            }

            if (coloring.Contains(false))
            {
                return false;
            }
            return true;

        }
        else
        {
            if (color == wantedColor)
            {
                return true;
            }
            else if (color != wantedColor)
            {
                return false;
            }
        }

        return false;
    }
}
