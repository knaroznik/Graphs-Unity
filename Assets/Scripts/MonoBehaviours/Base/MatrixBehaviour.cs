using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class MatrixBehaviour : MonoBehaviour {

	public GameObject VertexPrefab;
	public GameObject EdgePrefab;

    public GameObject DiEdgePrefab;

	public Text infoText;

	public Graph matrix;

	public bool DebugMode;
	public bool DiGraph;

	[Header("Paint materials")]
	public Material OriginalMaterial;
	public Material MarkedMaterial;

	void Awake () {
		if (DiGraph) {
			matrix = new DiGraph (VertexPrefab, DiEdgePrefab, OriginalMaterial, MarkedMaterial);
		} else {
			matrix = new Graph (VertexPrefab, EdgePrefab, OriginalMaterial, MarkedMaterial);
		}
	}

    public void Print()
    {
        if (DebugMode)
            infoText.text = matrix.Print();
    }

	public void CheckMinCycle(){
		int minValue = matrix.LowestValue ();
		if (minValue < 2) {
			infoText.text = matrix.Print () + " \n\n Za mały minimalny stopień aby znaleźć cykl! ";
			return;
		}
		minValue++;
		infoText.text = matrix.CheckCyclesOfLength (minValue);
	}

    public void ConstructDiGraph()
    {
        DiGraph = !DiGraph;
        OList<MathEdgeStruct> edgesCopy = GetEdges();
        matrix.Reset();

        if (DiGraph)
        {
            matrix = new DiGraph(VertexPrefab, DiEdgePrefab, OriginalMaterial, MarkedMaterial);
        }
        else
        {
            matrix = new Graph(VertexPrefab, EdgePrefab, OriginalMaterial, MarkedMaterial);
        }
        
        matrix.Construct(edgesCopy);
        Print();
    }

    public void GraphColor()
    {
        if (matrix.IsConsistent())
        {
            if (DiGraph)
            {
                ConstructDiGraph();
                return;
            }
            if (matrix.Color())
            {
                ConstructDiGraph();
                GetComponent<FlowAlgorithm>().Check(1);
                GetComponent<FlowAlgorithm>().Algorithm(10000000);
                infoText.text = "Maksymalne skojarzenie : " + ((DiGraph)matrix).GetAssociation();
                return;
            }
        }
    }

    private OList<MathEdgeStruct> GetEdges()
    {
        OList<MathEdgeStruct> edgesCopy = new OList<MathEdgeStruct>();

        OList<EdgeObject> edges = matrix.GetEdges();
        for(int i=0; i< edges.Count; i++)
        {
            edgesCopy.Add(edges[i].Copy());
        }

        return edgesCopy;
    }
}
