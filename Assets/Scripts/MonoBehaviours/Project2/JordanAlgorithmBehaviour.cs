using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JordanAlgorithmBehaviour : MonoBehaviour {

	public Graph matrix;

    public void Start()
    {
        matrix = GetComponent<MatrixBehaviour>().matrix;
    }

    /// <summary>
    /// Algorithm finds a tree center.
    /// Graph needs to be consistent and acyclic.
    /// </summary>
	public void JordanAlgorithm(){
		
		string output = "\n\n\tJORDAN ALGORITHM STARTING... \n\n";

		if (matrix.IsEmpty()) {
			output += "Graph is empty!";
			Write(output);
			return;
		}

        if (!matrix.IsConsistent ()) {
			output += "Graph is not consistent";
			Write(output);
			return;
		}

        if (matrix.cycleModule.HasCycle()){
            output += "Graph got cycle!";
            Write(output);
            return;
        }

        if (matrix.Count < 3){ //Check if tree got more than 2 vertexes, otherwise these vertexes are tree center.
            output += "Nucleus : " + matrix.WriteVertexes ();
			Write(output);
			return;
		}

		output += "Nucleus : " + FindNucleus ();

		//Wypisz wynik
		Write(output);
		return;
	}

	public void Write(string o){
		GetComponent<MatrixBehaviour>().infoText.text = matrix.Print() + o;
	}

    public string FindNucleus()
    {
        MathNeighborhoodMatrix mathMatrix = new MathNeighborhoodMatrix();
        mathMatrix.Construct(matrix);
        while (mathMatrix.vertexes.Count > 2)
        {
            OList<Vertex> toDelete = new OList<Vertex>();
            for (int i = 0; i < mathMatrix.vertexes.Count; i++)
            {
                if (mathMatrix.vertexes[i].Value() == 1)
                {
                    toDelete.Add(mathMatrix.vertexes[i]);
                }
            }

            for (int i = 0; i < toDelete.Count; i++)
            {
                mathMatrix.RemoveNamedVertex(toDelete[i].VertexName);
            }
        }
        string result = String.Join(" ", mathMatrix.vertexes.ToList().Select(item => item.ToString()).ToArray());
        return result;

    }


}
