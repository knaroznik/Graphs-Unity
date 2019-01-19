using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinCycleAlgorithmBehaviour : MonoBehaviour {

    private Graph matrix;

	// Use this for initialization
	void Start () {
        matrix = GetComponent<MatrixBehaviour>().matrix;
	}
	
    public void Algorithm()
    {
        int minValue = matrix.info.LowestValue();
        if (minValue < 2)
        {
            Write(" \n\n Za mały minimalny stopień aby znaleźć cykl! ");
            return;
        }
        minValue++;
        Write(CheckCyclesOfLength(minValue));
    }

    public void Write(string o)
    {
        GetComponent<MatrixBehaviour>().infoText.text = matrix.info.Print() + o;
    }

    public string CheckCyclesOfLength(int x)
    {
        string output = "";
        output += "\n\n FINDING CYCLE OF MIN(deg(matrix) + 1) \n\n ";
        output += "\nStarting from C" + x;
        for (int i = x; i <= matrix.Size; i++)
        {
            OList<Vertex> cycle = naiveCycles(i);
            if (cycle != null)
            {
                string result = String.Join("->", cycle.ToList().Select(item => item.ToString()).ToArray());
                output += "\nFound cycle C" + i + " :\n";
                output += result;
                matrix.brush.Paint(cycle);
                break;
            }
        }
        return output;
    }

    protected OList<Vertex> naiveCycles(int x)
    {
        for (int i = 0; i < matrix.vertexes.Count; i++)
        {
            OList<Vertex> q = matrix.cycleModule.findCycleLength(i, new OList<Vertex>(), i, x);
            if (q != null)
            {
                return q;
            }
        }
        return null;
    }

    
}
