using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationModule {

    protected OList<Vertex> vertexes;

    public LocationModule(Graph _graph)
    {
        vertexes = _graph.vertexes;
    }

	private OList<Vertex> FindUnseenBorderers(Vertex _currentVertex, OList<Vertex> _visited){
		OList<Vertex> output = new OList<Vertex> ();
		for (int i = 0; i < vertexes.Count; i++) {
			int current = vertexes.IndexOf (new Vertex (_currentVertex.VertexName));
			if (vertexes [current] [i] == 1) {
				if (!_visited.ToList ().Contains (vertexes [i])) {
					output.Add (vertexes [i]);
				}
			}
		}

		return output;
	}
	/// <summary>
	/// Return all edges in graph. No repetitions.
	/// </summary>
	/// <returns>All edges in graph</returns>
	public OList<EdgeObject> GetEdges(){
		OList<EdgeObject> output = new OList<EdgeObject> ();
		for (int i = 0; i < vertexes.Count; i++) {
			int edges = vertexes[i].VertexObject.GetComponent<VertexObject> ().EdgeCount;
			for (int j = 0; j < edges; j++) {
				AddEdge (ref output, vertexes[i].VertexObject.GetComponent<VertexObject> ().Edge (j));
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

    public OList<Vertex> FindBorderers(Vertex _currentVertex)
    {
        OList<Vertex> output = new OList<Vertex>();
        for (int i = 0; i < vertexes.Count; i++)
        {
            int current = vertexes.IndexOf(new Vertex(_currentVertex.VertexName));
            if (vertexes[current][i] > 0)
            {
                output.Add(vertexes[i]);
            }
        }

        return output;
    }

    #region DFS 

    public OList<EdgeStruct> DFS(){
		Vertex currentVertex;
		Stack<Vertex> stack = new Stack<Vertex> ();
		OList<EdgeStruct> treeEdges = new OList<EdgeStruct> ();
		OList<Vertex> visitedVertexes = new OList<Vertex> ();

		currentVertex = visitVertex(vertexes[0], ref stack, ref visitedVertexes);

		while (!stack.IsEmpty ()) {

			OList<Vertex> borderers = FindUnseenBorderers (currentVertex, visitedVertexes);
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
