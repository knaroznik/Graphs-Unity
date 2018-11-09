using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructModule {

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

	public void InsertEdges(OList<EdgeStruct> _treeEdges, NeighborhoodMatrix _matrix){
		for (int i = 0; i < _treeEdges.Count; i++) {
			_matrix.AddEdge (_treeEdges [i].FirstPoint().VertexName, _treeEdges [i].SecondPoint().VertexName);
		}
	}
}
