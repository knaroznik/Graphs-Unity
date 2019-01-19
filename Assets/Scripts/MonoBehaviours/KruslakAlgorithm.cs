using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KruslakAlgorithm : MonoBehaviour {

	public Graph matrix;

	public void Algorithm(){
		matrix = GetComponent<MatrixBehaviour> ().matrix;

		if (!matrix.IsConsistent () || matrix.vertexes.Count < 2) {
			return;
		}

		OList<EdgeObject> edges = matrix.locationModule.GetEdges ();
		List<EdgeObject> edgesList = edges.ToList ();
		edgesList = edgesList.OrderBy (x => x.EdgeCost).ToList();
		KruskalEdges (edgesList);

		GetComponent<MatrixBehaviour> ().infoText.text = matrix.info.Print ();
	}

	private void KruskalEdges(List<EdgeObject> edges){
		OList<CostEdgeStruct> edgesCopy = new OList<CostEdgeStruct> ();
		for (int i = 0; i < edges.Count; i++) {
			edgesCopy.Add (new CostEdgeStruct (edges [i].obj1.vertexData, edges [i].obj2.vertexData, edges[i].EdgeCost));
		}
		matrix.ResetEdges ();
		for (int i = 0; i < edgesCopy.Count; i++) {
			matrix.AddEdge (edgesCopy [i].FirstPoint ().VertexName, edgesCopy [i].SecondPoint ().VertexName, edgesCopy [i].EdgeCost);

			if (matrix.cycleModule.HasCycle()) {
				matrix.RemoveEdge (edgesCopy [i].FirstPoint ().VertexName, edgesCopy [i].SecondPoint ().VertexName);
			}
		}
	}

}
