using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationModule {

	private OList<Vertex> FindUnseenBorderers(Vertex _currentVertex, OList<Vertex> _visited, Graph _matrix){
		OList<Vertex> output = new OList<Vertex> ();
		for (int i = 0; i < _matrix.vertexes.Count; i++) {
			int current = _matrix.vertexes.IndexOf (new Vertex (_currentVertex.VertexName));
			if (_matrix.vertexes [current] [i] == 1) {
				if (!_visited.ToList ().Contains (_matrix.vertexes [i])) {
					output.Add (_matrix.vertexes [i]);
				}
			}
		}

		return output;
	}
	/// <summary>
	/// Return all edges in graph. No repetitions.
	/// </summary>
	/// <returns>All edges in graph</returns>
	/// <param name="_vertexes">Graph represented by neighborhood matrix.</param>
	public OList<EdgeObject> GetEdges(OList<Vertex> _vertexes){
		OList<EdgeObject> output = new OList<EdgeObject> ();
		for (int i = 0; i < _vertexes.Count; i++) {
			int edges = _vertexes [i].VertexObject.GetComponent<VertexObject> ().EdgeCount;
			for (int j = 0; j < edges; j++) {
				AddEdge (ref output, _vertexes [i].VertexObject.GetComponent<VertexObject> ().Edge (j));
			}

		}
		return output;
	}

	/// <summary>
	/// Adds the edge to referenced list, if only edge is not there already.
	/// </summary>
	/// <param name="edges">Reference to list in which we want edge.</param>
	/// <param name="newEdge">Edge needed to be checked.</param>
	private void AddEdge(ref OList<EdgeObject> edges, EdgeObject newEdge){
		for (int i = 0; i < edges.Count; i++) {
			if(newEdge.IsSame(edges[i])){
				return;
			}
		}
		edges.Add (newEdge);
	}

    public OList<Vertex> FindBorderers(Vertex _currentVertex, Graph _matrix)
    {
        OList<Vertex> output = new OList<Vertex>();
        for (int i = 0; i < _matrix.vertexes.Count; i++)
        {
            int current = _matrix.vertexes.IndexOf(new Vertex(_currentVertex.VertexName));
            if (_matrix.vertexes[current][i] > 0)
            {
                output.Add(_matrix.vertexes[i]);
            }
        }

        return output;
    }

    #region DFS 

    public OList<EdgeStruct> DFS(Graph _matrix){
		Vertex currentVertex;
		Stack<Vertex> stack = new Stack<Vertex> ();
		OList<EdgeStruct> treeEdges = new OList<EdgeStruct> ();
		OList<Vertex> visitedVertexes = new OList<Vertex> ();

		currentVertex = visitVertex(_matrix.vertexes[0], ref stack, ref visitedVertexes);

		while (!stack.IsEmpty ()) {

			OList<Vertex> borderers = FindUnseenBorderers (currentVertex, visitedVertexes, _matrix);
			if (borderers.Count > 0) {
				treeEdges.Add (new EdgeStruct (currentVertex, borderers [0]));
				currentVertex = visitVertex (borderers [0], ref stack, ref visitedVertexes);
			} else {
				stack.Pop ();
				if(!stack.IsEmpty())
					currentVertex = stack.Last ();
			}
		}

		return treeEdges;
	}

	protected Vertex visitVertex(Vertex addVertex, ref Stack<Vertex> _stack, ref OList<Vertex> _visitedVertexes){
		_stack.Push (addVertex);
		_visitedVertexes.Add (addVertex);
		return addVertex;
	}

	#endregion
}
