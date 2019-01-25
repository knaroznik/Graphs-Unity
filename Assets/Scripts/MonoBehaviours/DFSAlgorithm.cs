using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSAlgorithm : MonoBehaviour {

	public Graph matrix;

	public void Algorithm(){
		matrix = GetComponent<MatrixBehaviour> ().matrix;

		if (!matrix.IsConsistent ()) {
			GetComponent<MatrixBehaviour> ().infoText.text = matrix.info.Print() + " \n\nDFS STARTING...\n\n Graph is not consistent or is empty!";
			return;
		}

		OList<EdgeStruct> treeEdges = matrix.locationModule.DFS ();

		matrix.construct.ResetEdges ();
		matrix.construct.InsertEdges (treeEdges);

		GetComponent<MatrixBehaviour> ().infoText.text = matrix.info.Print () + " \n\nDFS STARTING...\n\n DFS found tree!";;
	}
}
