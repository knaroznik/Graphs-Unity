using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructModule {

	protected GameObject vertexPrefab;
	protected GameObject edgePrefab;
	protected PaintModule brush;
    protected OList<Vertex> vertexes;
    protected bool twoWayEdges;

    public ConstructModule(GameObject _vertexPrefab, GameObject _edgePrefab, PaintModule _brush, Graph _graph, bool _twoWayEdges){
		vertexPrefab = _vertexPrefab;
		edgePrefab = _edgePrefab;
		brush = _brush;
        vertexes = _graph.vertexes;
        twoWayEdges = _twoWayEdges;
    }

    public void AddEdge(int x, int y)
    {

        if (brush != null)
        {
            brush.Reset();
        }
        vertexes[x][y] += 1;
        if (twoWayEdges)
        {
            vertexes[y][x] += 1;
        }
    }

    public void RemoveEdge(int x, int y)
    {
        if (brush != null)
        {
            brush.Reset();
        }
        vertexes[x][y] -= 1;
        if (twoWayEdges)
        {
            vertexes[y][x] -= 1;
        }
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
		for (int i = 0; i < _edges.Count; i++) {
            AddEdge (_edges [i].FirstPoint().VertexName, _edges [i].SecondPoint().VertexName, 1);
		}
	}

	public void AddVertex(string _newVertexName, Vector3? vertexPosition = null){

		if (VertexExists(_newVertexName)) {
			return;
		}

		if (brush != null) {
			brush.Reset ();
		}

        GameObject vertex;
        if (vertexPosition == null)
        {
            vertex = MonoBehaviour.Instantiate(vertexPrefab, new Vector3(UnityEngine.Random.Range(-7f, 7f), UnityEngine.Random.Range(-7f, 7f), 0f), Quaternion.identity);
        }
        else
        {
            vertex = MonoBehaviour.Instantiate(vertexPrefab, vertexPosition.Value, Quaternion.identity);
        }

        for (int i = 0; i < vertexes.Count; i++) {
            vertexes.Get (i).AddPossibility (vertex);
		}

        vertexes.Add (new Vertex (vertex, _newVertexName));
        vertexes.Get (vertexes.Count - 1).AddPossibilities (vertexes.Count, vertexes);
	}

	public void RemoveVertex(string _vertexName){
		int vertexNumber = vertexes.IndexOf (new Vertex (_vertexName));

		if (vertexNumber == -1)
			return;


		for (int i = 0; i < vertexes.Count; i++) {
            vertexes[i].RemoveAt (vertexNumber);
		}
		if (brush != null) {
			brush.Reset ();
		}

        vertexes[vertexNumber].VertexObject.GetComponent<VertexObject> ().Destroy ();
        vertexes.RemoveAt (vertexNumber);
	}

	private bool VertexExists(string _vertexName)
    {
        return vertexes.IndexOf(new Vertex(_vertexName)) != -1;
    }
    
    public void AddEdge(string one, string two, string edgeCost, Operator sign = Operator.PLUS){
		int x = vertexes.IndexOf (new Vertex (one));
		int y = vertexes.IndexOf (new Vertex (two));
		int cost = 0;
		int.TryParse (edgeCost, out cost);

		if (edgeCost == "" || edgeCost == null) {
			cost = 1;
		}
        AddEdge(x, y, cost, sign);

    }

	public void AddEdge(string one, string two, int edgeCost, Operator sign = Operator.PLUS)
    {
		int x = vertexes.IndexOf (new Vertex (one));
		int y = vertexes.IndexOf (new Vertex (two));
        AddEdge(x, y, edgeCost, sign);
    }

    public void AddEdge(int one, int two, int edgeCost, Operator sign)
    {
        if (one == -1 || two == -1 || edgeCost == 0)
            return;

        AddEdge(one, two);

        GameObject line = MonoBehaviour.Instantiate(edgePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        line.GetComponent<EdgeObject>().Init(vertexes.Get(one).VertexObject.GetComponent<VertexObject>(), vertexes.Get(two).VertexObject.GetComponent<VertexObject>(), edgeCost);
        line.GetComponent<EdgeObject>().Sign = sign;
    }

	public void RemoveEdge(string one, string two){
		int x = vertexes.IndexOf (new Vertex (one));
		int y = vertexes.IndexOf (new Vertex (two));
		if(x == -1 || y == -1 || vertexes[x] [y] == 0)
			return;
		RemoveEdge (x, y);
        vertexes.Get (x).VertexObject.GetComponent<VertexObject> ().RemoveEdgeWith (vertexes.Get (y).VertexObject.GetComponent<VertexObject> ());
	}

    public void AddNewVertex(Vector3 _vertexPosition, string _vertexName = "")
    {
        if (_vertexName == "")
        {
            int newVertexName = 0;

            while (vertexes.IndexOf(new Vertex(newVertexName.ToString())) != -1)
            {
                newVertexName++;
            }
            AddVertex(newVertexName.ToString(), _vertexPosition);
        }
        else
        {
            AddVertex(_vertexName, _vertexPosition);
        }
    }

    public void Reset()
    {
        ResetEdges();
        resetVertexes();
    }

    public void ResetEdges()
    {
        for (int i = 0; i < vertexes.Count; i++)
        {
            for (int j = 0; j < vertexes.Count; j++)
            {
                if (vertexes[i][j] > 0)
                {
                    int edgeCount = vertexes[i][j];
                    for (int x = 0; x < edgeCount; x++)
                    {
                        RemoveEdge(vertexes[i].VertexName, vertexes[j].VertexName);
                    }
                }
            }
        }
    }

    protected void resetVertexes()
    {
        int x = vertexes.Count;
        for (int i = 0; i < x; i++)
        {
            RemoveVertex(vertexes[0].VertexName);
        }
    }

}
