using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JordanAlgorithmBehaviour : MonoBehaviour {

	public NeighborhoodMatrix matrix;

	public void JordanAlgorithm(){
		matrix = GetComponent<MatrixBehaviour> ().matrix;
		string output = "\n\n\tJORDAN ALGORITHM STARTING... \n\n";

		//Check if graph exists
		if (matrix.Count == 0) {
			output += "Graph is empty!";
			Write(output);
			return;
		}

		//Check if graph is consistent
		if (!matrix.IsConsistent ()) {
			output += "Graph is not consistent";
			Write(output);
			return;
		}

		//Sprawdz czy ma więcej niż 2 węzły, jeśli nie, zwróć te węzły jako jądro

		if (matrix.Count < 3) {
			output += "Nucleus : " + matrix.WriteVertexes ();
			Write(output);
			return;
		}

		//Sprawdz czy jest acykliczny

		if (matrix.HasCycle ()) {
			output += "Graph got cycle!";
			Write(output);
			return;
		}

		//Usuwaj liście, aż zostanie mniej niż 3 węzły, jeśli są mniej niż 3 węzły zwróć je.

		output += "Nucleus : " + matrix.Nucleus.FindNucleus (matrix);

		//Wypisz wynik
		Write(output);
		return;
	}

	public void Write(string o){
		GetComponent<MatrixBehaviour>().infoText.text = matrix.Print() + o;
	}


}
