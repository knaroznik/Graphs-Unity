using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KosarajuAlgorithm : MonoBehaviour {

	public DiGraph matrix;

	public void Algorithm(){
		matrix = GetComponent<MatrixBehaviour> ().matrix as DiGraph;

		Stack<Vertex> globalStack = matrix.FullVertexesStack ();
		OList<OList<Vertex>> consistencyParts = new OList<OList<Vertex>> ();
		OList<Vertex> visitedVertexes = new OList<Vertex> ();

		MathDiGraph graphCopy = new MathDiGraph ();
        graphCopy.Construct(matrix);
		graphCopy.Transpond ();

		while (!globalStack.IsEmpty ()) {
			Vertex current = globalStack.Last ();
			globalStack.Pop ();

			if(visitedVertexes.ToList().Contains(current)){
				continue;
			}

			OList<Vertex> consistencyPart = graphCopy.GetConsistencyPartOf (current);
			for (int i = 0; i < consistencyPart.Count; i++) {
                Vertex temp = consistencyPart[i];
                visitedVertexes.Add (temp);
                consistencyPart[i] = matrix.vertexes[matrix.vertexes.IndexOf(temp)];
                
			}
			consistencyParts.Add (consistencyPart);
		}
		matrix.PaintConsistency (consistencyParts);

	}
}
