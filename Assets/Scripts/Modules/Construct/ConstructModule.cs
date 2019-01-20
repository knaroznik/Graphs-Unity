using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructModule {

	protected GameObject vertexPrefab;
	protected GameObject edgePrefab;
	protected PaintModule brush;
    protected Graph graph;

	public ConstructModule(GameObject _vertexPrefab, GameObject _edgePrefab, PaintModule _brush, Graph _graph){
		vertexPrefab = _vertexPrefab;
		edgePrefab = _edgePrefab;
		brush = _brush;
        graph = _graph;
	}

	public void ResetEdges(){
		for (int i = 0; i < graph.vertexes.Count; i++) {
			for (int j = 0; j < graph.vertexes.Count; j++) {
				if (graph.vertexes [i] [j] > 0) {
					int edgeCount = graph.vertexes [i] [j];
					for (int x = 0; x < edgeCount; x++) {
                        RemoveEdge (graph.vertexes [i].VertexName, graph.vertexes [j].VertexName);
					}
				}
			}
		}
	}

    public void ResetVertexes()
    {
        int x = graph.vertexes.Count;
        for (int i=0; i< x; i++)
        {
            RemoveVertex(graph.vertexes[0].VertexName);
        }
    }

	public void InsertEdges(OList<EdgeStruct> _edges){
		for (int i = 0; i < _edges.Count; i++) {
            AddEdge (_edges [i].FirstPoint().VertexName, _edges [i].SecondPoint().VertexName, 1);
		}
	}

	public void AddVertex(string _newVertexName, Vector3? vertexPosition = null){

		if (graph.vertexes.IndexOf (new Vertex (_newVertexName)) != -1) {
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
            vertex = MonoBehaviour.Instantiate(vertexPrefab, vertexPosition.GetValueOrDefault(), Quaternion.identity);
        }
        for (int i = 0; i < graph.Size; i++) {
            graph.vertexes.Get (i).AddPossibility (vertex);
		}

        graph.vertexes.Add (new Vertex (vertex, _newVertexName));

        graph.vertexes.Get (graph.vertexes.Count - 1).AddPossibilities (graph.vertexes.Count, graph.vertexes);
	}

	public void RemoveVertex(string _vertexName){
		int vertexNumber = graph.vertexes.IndexOf (new Vertex (_vertexName));

        for(int i=0; i< graph.vertexes[vertexNumber].VertexObject.GetComponent<VertexObject>().EdgeCount; i++)
        {
            EdgeObject edge = graph.vertexes[vertexNumber].VertexObject.GetComponent<VertexObject>().Edge(i);
            edge.obj1.vertexData.OutEdges--;
            edge.obj2.vertexData.InEdges++;
        }

		if (vertexNumber == -1)
			return;


		for (int i = 0; i < graph.vertexes.Count; i++) {
            graph.vertexes[i].RemoveAt (vertexNumber);
		}
		if (brush != null) {
			brush.Reset ();
		}

        graph.vertexes[vertexNumber].VertexObject.GetComponent<VertexObject> ().Destroy ();
        graph.vertexes.RemoveAt (vertexNumber);
	}

	public virtual void AddEdge(int x, int y){
		if (brush != null) {
			brush.Reset ();
		}
        graph.vertexes[x] [y] += 1;
        graph.vertexes[y] [x] += 1;

        graph.vertexes[x].OutEdges++;
        graph.vertexes[y].InEdges++;
	}

	public void AddEdge(string one, string two, string edgeCost, Operator sign = Operator.PLUS){
		int x = graph.vertexes.IndexOf (new Vertex (one));
		int y = graph.vertexes.IndexOf (new Vertex (two));
		int cost = 0;
		int.TryParse (edgeCost, out cost);

		if (edgeCost == "" || edgeCost == null) {
			cost = 1;
		}

		if(x == -1 || y == -1|| cost == 0)
			return;
		AddEdge (x, y);
        

		GameObject line = MonoBehaviour.Instantiate (edgePrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		line.GetComponent<EdgeObject> ().Init (graph.vertexes.Get (x).VertexObject.GetComponent<VertexObject>(), graph.vertexes.Get (y).VertexObject.GetComponent<VertexObject>(), cost);
        line.GetComponent<EdgeObject>().Sign = sign;

    }

	public void AddEdge(string one, string two, int edgeCost, Operator sign = Operator.PLUS)
    {
		int x = graph.vertexes.IndexOf (new Vertex (one));
		int y = graph.vertexes.IndexOf (new Vertex (two));
		if(x == -1 || y == -1)
			return;
		AddEdge (x, y);

		GameObject line = MonoBehaviour.Instantiate (edgePrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		line.GetComponent<EdgeObject> ().Init (graph.vertexes.Get (x).VertexObject.GetComponent<VertexObject>(), graph.vertexes.Get (y).VertexObject.GetComponent<VertexObject>(), edgeCost);
        line.GetComponent<EdgeObject>().Sign = sign;
    }

	public virtual void RemoveEdge(int x, int y){
		if (brush != null) {
			brush.Reset ();
		}
        graph.vertexes[x] [y] -= 1;
        graph.vertexes[y] [x] -= 1;

        graph.vertexes[x].OutEdges--;
        graph.vertexes[y].InEdges--;
    }

	public void RemoveEdge(string one, string two){
		int x = graph.vertexes.IndexOf (new Vertex (one));
		int y = graph.vertexes.IndexOf (new Vertex (two));
		if(x == -1 || y == -1 || graph.vertexes[x] [y] == 0)
			return;
		RemoveEdge (x, y);
        graph.vertexes.Get (x).VertexObject.GetComponent<VertexObject> ().RemoveEdgeWith (graph.vertexes.Get (y).VertexObject.GetComponent<VertexObject> ());
	}

    public void AddNewVertex(Vector3 _vertexPosition, string _vertexName = "")
    {
        if (_vertexName == "")
        {
            int newVertexName = 0;

            while (graph.vertexes.IndexOf(new Vertex(newVertexName.ToString())) != -1)
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
}
