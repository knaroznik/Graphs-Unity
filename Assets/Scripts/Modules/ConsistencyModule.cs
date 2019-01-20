using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConsistencyModule {

    private OList<Vertex> vertexes;
    private PaintModule brush;

    public ConsistencyModule(Graph _graph, PaintModule _brush)
    {
        vertexes = _graph.vertexes;
        brush = _brush;
    }

	public bool IsConsistent(){
		if (vertexes.Count < 1) {
			return false;
		}

		List<Vertex> graph = new List<Vertex> ();
		GraphPassage (0, graph);
		if (graph.Count == vertexes.Count) {
			return true;
		}
		return false;
	}
    
    public List<Vertex> GetConnectedVertexes(int startedVertex)
    {
        List<Vertex> graph = new List<Vertex>();
        GraphPassage(startedVertex, graph);
        return graph;
    }

	private void GraphPassage(int current, List<Vertex> visited)
	{
		visited.Add (vertexes[current]);
		for (int i = 0; i < vertexes.Count; i++) {
			if (vertexes [current] [i] > 0) {
				if (!visited.ToList().Contains (vertexes [i])) {
					GraphPassage (i, visited);
				}
			}
		}
	}

	private void GraphPassage(int current, ref OList<Vertex> visited, List<Vertex> visitedGlobal)
	{
		visited.Add (vertexes[current]);
		visitedGlobal.Add (vertexes [current]);
		for (int i = 0; i < vertexes.Count; i++) {
			if (vertexes [current] [i] > 0) {
				if (!visited.ToList().Contains (vertexes [i]) && !visitedGlobal.ToList().Contains (vertexes [i])) {
					GraphPassage (i, ref visited, visitedGlobal);
				}
			}
		}
	}

	public void PaintConsistency(){
		if (vertexes.Count < 1) {
			return;
		}
		int foundVertexes = 0;
		OList<OList<Vertex>> consistencyParts = new OList<OList<Vertex>>();
		List<Vertex> visitedVertexes = new List<Vertex> ();
		while (foundVertexes < vertexes.Count) {
			OList<Vertex> graph = new OList<Vertex> ();
			GraphPassageStart (ref graph, visitedVertexes);
			foundVertexes += graph.Count;
			consistencyParts.Add (graph);
		}
		brush.Paint (consistencyParts);
		return;
	}

	private void GraphPassageStart(ref OList<Vertex> visited, List<Vertex> visitedGlobal){
		for (int i = 0; i < vertexes.Count; i++) {
			if (!visitedGlobal.ToList ().Contains (vertexes [i])) {
				GraphPassage (i, ref visited, visitedGlobal);
				return;
			}
		}
	}
}
