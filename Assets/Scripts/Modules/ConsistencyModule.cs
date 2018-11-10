using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConsistencyModule {

	public bool IsConsistent(OList<Vertex> _vertexes){
		if (_vertexes.Count < 1) {
			return false;
		}

		List<Vertex> graph = new List<Vertex> ();
		GraphPassage (0, graph, _vertexes);
		if (graph.Count == _vertexes.Count) {
			return true;
		}
		return false;
	}

	private void GraphPassage(int current, List<Vertex> visited, OList<Vertex> _vertexes)
	{
		visited.Add (_vertexes[current]);
		for (int i = 0; i < _vertexes.Count; i++) {
			if (_vertexes [current] [i] > 0) {
				if (!visited.ToList().Contains (_vertexes [i])) {
					GraphPassage (i, visited, _vertexes);
				}
			}
		}
	}

	private void GraphPassage(int current, ref OList<Vertex> visited, OList<Vertex> _vertexes, List<Vertex> visitedGlobal)
	{
		visited.Add (_vertexes[current]);
		visitedGlobal.Add (_vertexes [current]);
		for (int i = 0; i < _vertexes.Count; i++) {
			if (_vertexes [current] [i] > 0) {
				if (!visited.ToList().Contains (_vertexes [i]) && !visitedGlobal.ToList().Contains (_vertexes [i])) {
					GraphPassage (i, ref visited, _vertexes, visitedGlobal);
				}
			}
		}
	}

	public void PaintConsistency(OList<Vertex> _vertexes, PaintModule brush){
		if (_vertexes.Count < 1) {
			return;
		}
		int foundVertexes = 0;
		OList<OList<Vertex>> consistencyParts = new OList<OList<Vertex>>();
		List<Vertex> visitedVertexes = new List<Vertex> ();
		while (foundVertexes < _vertexes.Count) {
			OList<Vertex> graph = new OList<Vertex> ();
			GraphPassageStart (ref graph, _vertexes, visitedVertexes);
			foundVertexes += graph.Count;
			consistencyParts.Add (graph);
		}
		brush.Paint (consistencyParts);
		return;
	}

	private void GraphPassageStart(ref OList<Vertex> visited, OList<Vertex> _vertexes, List<Vertex> visitedGlobal){
		for (int i = 0; i < _vertexes.Count; i++) {
			if (!visitedGlobal.ToList ().Contains (_vertexes [i])) {
				GraphPassage (i, ref visited, _vertexes, visitedGlobal);
				return;
			}
		}
	}
}
