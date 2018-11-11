using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsistencyAlgoritm : MonoBehaviour {

	public NeighborhoodMatrix matrix;

	public void Algorithm(){
		matrix = GetComponent<MatrixBehaviour> ().matrix;

//		if (!matrix.IsConsistent ()) {
//			GetComponent<MatrixBehaviour> ().infoText.text = matrix.Print () + " \n\nDFS STARTING...\n\n Graph is not consistent or is empty!";
//			return;
//		}

		matrix.PaintConsistency ();
	}
}
