using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationModule {

	public OList<Vertex> FindUnseenBorderer(Vertex _currentVertex, OList<Vertex> _visited, NeighborhoodMatrix _matrix){
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
}
