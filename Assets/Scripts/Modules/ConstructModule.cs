using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructModule {

	private GameObject vertexPrefab;
	private GameObject edgePrefab;
	private PaintModule brush;

	public ConstructModule(GameObject _vertexPrefab, GameObject _edgePrefab, PaintModule _brush){
		vertexPrefab = _vertexPrefab;
		edgePrefab = _edgePrefab;
		brush = _brush;
	}

	public void ResetEdges(NeighborhoodMatrix _matrix){
		for (int i = 0; i < _matrix.vertexes.Count; i++) {
			for (int j = 0; j < _matrix.vertexes.Count; j++) {
				if (_matrix.vertexes [i] [j] > 0) {
					int edgeCount = _matrix.vertexes [i] [j];
					for (int x = 0; x < edgeCount; x++) {
						_matrix.RemoveEdge (_matrix.vertexes [i].VertexName, _matrix.vertexes [j].VertexName);
					}
				}
			}
		}
	}

	public void InsertEdges(OList<EdgeStruct> _edges, NeighborhoodMatrix _matrix){
		for (int i = 0; i < _edges.Count; i++) {
			_matrix.AddEdge (_edges [i].FirstPoint().VertexName, _edges [i].SecondPoint().VertexName, 1);
		}
	}

	public void AddVertex(string _newVertexName, ref OList<Vertex> _vertexes){

		if (_vertexes.IndexOf (new Vertex (_newVertexName)) != -1) {
			return;
		}

		if (brush != null) {
			brush.Reset ();
		}

		GameObject vertex = MonoBehaviour.Instantiate (vertexPrefab, new Vector3 (UnityEngine.Random.Range (-7f, 7f), UnityEngine.Random.Range (-7f, 7f), 0f), Quaternion.identity);
		for (int i = 0; i < _vertexes.Count; i++) {
			_vertexes.Get (i).AddPossibility (vertex);
		}

		_vertexes.Add (new Vertex (vertex, _newVertexName));

		_vertexes.Get (_vertexes.Count - 1).AddPossibilities (_vertexes.Count, _vertexes);
	}

	public void RemoveVertex(string _vertexName, ref OList<Vertex> _vertexes){
		int vertexNumber = _vertexes.IndexOf (new Vertex (_vertexName));

		if (vertexNumber == -1)
			return;


		for (int i = 0; i < _vertexes.Count; i++) {
			_vertexes [i].RemoveAt (vertexNumber);
		}
		if (brush != null) {
			brush.Reset ();
		}

		_vertexes [vertexNumber].VertexObject.GetComponent<VertexObject> ().Destroy ();
		_vertexes.RemoveAt (vertexNumber);
	}

	public void AddEdge(int x, int y, ref OList<Vertex> _vertexes){
		if (brush != null) {
			brush.Reset ();
		}
		_vertexes [x] [y] += 1;
		_vertexes [y] [x] += 1;
	}

	public void AddEdge(string one, string two, string edgeCost, ref OList<Vertex> _vertexes){
		int x = _vertexes.IndexOf (new Vertex (one));
		int y = _vertexes.IndexOf (new Vertex (two));
		int cost = 0;
		int.TryParse (edgeCost, out cost);

		if (edgeCost == "" || edgeCost == null) {
			cost = 1;
		}

		if(x == -1 || y == -1|| cost == 0)
			return;
		AddEdge (x, y, ref _vertexes);

		GameObject line = MonoBehaviour.Instantiate (edgePrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		line.GetComponent<EdgeObject> ().Init (_vertexes.Get (x).VertexObject.GetComponent<VertexObject>(), _vertexes.Get (y).VertexObject.GetComponent<VertexObject>(), cost);
	}

	public void AddEdge(string one, string two, int edgeCost, ref OList<Vertex> _vertexes){
		int x = _vertexes.IndexOf (new Vertex (one));
		int y = _vertexes.IndexOf (new Vertex (two));
		if(x == -1 || y == -1|| edgeCost == 0)
			return;
		AddEdge (x, y, ref _vertexes);

		GameObject line = MonoBehaviour.Instantiate (edgePrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		line.GetComponent<EdgeObject> ().Init (_vertexes.Get (x).VertexObject.GetComponent<VertexObject>(), _vertexes.Get (y).VertexObject.GetComponent<VertexObject>(), edgeCost);
	}

	public void RemoveEdge(int x, int y, ref OList<Vertex> _vertexes){
		if (brush != null) {
			brush.Reset ();
		}
		_vertexes [x] [y] -= 1;
		_vertexes [y] [x] -= 1;
	}

	public void RemoveEdge(string one, string two, ref OList<Vertex> _vertexes){
		int x = _vertexes.IndexOf (new Vertex (one));
		int y = _vertexes.IndexOf (new Vertex (two));
		if(x == -1 || y == -1 || _vertexes [x] [y] == 0)
			return;
		RemoveEdge (x, y, ref _vertexes);
		_vertexes.Get (x).VertexObject.GetComponent<VertexObject> ().RemoveEdgeWith (_vertexes.Get (y).VertexObject.GetComponent<VertexObject> ());
	}

}
