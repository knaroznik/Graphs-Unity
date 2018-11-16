using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class NucleusModule {

	public string FindNucleus(Graph matrix){
		MathNeighborhoodMatrix mathMatrix = new MathNeighborhoodMatrix ();
		mathMatrix.Construct (matrix);
		while (mathMatrix.vertexes.Count > 2) {
			OList<Vertex> toDelete = new OList<Vertex> ();
			for (int i = 0; i < mathMatrix.vertexes.Count; i++) {
				if (mathMatrix.vertexes [i].Value() == 1) {
					toDelete.Add (mathMatrix.vertexes [i]);
				}
			}

			for (int i = 0; i < toDelete.Count; i++) {
				mathMatrix.RemoveNamedVertex (toDelete [i].VertexName);
			}
		}
		string result = String.Join(" ", mathMatrix.vertexes.ToList().Select(item => item.ToString()).ToArray());
		return result;

	}
}
