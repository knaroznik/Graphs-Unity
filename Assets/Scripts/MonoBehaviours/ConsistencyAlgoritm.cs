using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsistencyAlgoritm : MonoBehaviour {

	private Graph matrix;

	public void Algorithm(){
		matrix = GetComponent<MatrixBehaviour> ().matrix;
		matrix.consistency.PaintConsistency ();
	}
}
