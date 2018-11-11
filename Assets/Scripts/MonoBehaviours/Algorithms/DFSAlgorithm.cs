using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSAlgorithm : MonoBehaviour {

	public NeighborhoodMatrix matrix;

	public void Algorithm(){
		matrix = GetComponent<MatrixBehaviour> ().matrix;

		if (!matrix.IsConsistent ()) {
			GetComponent<MatrixBehaviour> ().infoText.text = matrix.Print() + " \n\nDFS STARTING...\n\n Graph is not consistent or is empty!";
			return;
		}

		Vertex currentVertex;
		Stack<Vertex> stack = new Stack<Vertex> ();
		OList<EdgeStruct> treeEdges = new OList<EdgeStruct> ();
		OList<Vertex> visitedVertexes = new OList<Vertex> ();

		currentVertex = visitVertex(matrix.vertexes[0], ref stack, ref visitedVertexes);

		while (!stack.IsEmpty ()) {

			OList<Vertex> borderers = matrix.FindUnseenBorderers (currentVertex, visitedVertexes);
			if (borderers.Count > 0) {
				treeEdges.Add (new EdgeStruct (currentVertex, borderers [0]));
				currentVertex = visitVertex (borderers [0], ref stack, ref visitedVertexes);
			} else {
				stack.Pop ();
				if(!stack.IsEmpty())
					currentVertex = stack.Last ();
			}
		}

		matrix.ResetEdges ();
		matrix.InsertEdges (treeEdges);

		GetComponent<MatrixBehaviour> ().infoText.text = matrix.Print () + " \n\nDFS STARTING...\n\n DFS found tree!";;
	}

	private Vertex visitVertex(Vertex addVertex, ref Stack<Vertex> _stack, ref OList<Vertex> _visitedVertexes){
		_stack.Push (addVertex);
		_visitedVertexes.Add (addVertex);
		return addVertex;
	}
}
