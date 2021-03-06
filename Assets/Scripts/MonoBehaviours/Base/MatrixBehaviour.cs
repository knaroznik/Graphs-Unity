﻿using System.Collections;
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
            infoText.text = matrix.info.Print();
    }

    public void ConstructDiGraph()
    {
        DiGraph = !DiGraph;
        OList<MathEdgeStruct> edgesCopy = GetEdges();
        matrix.construct.Reset();

        if (DiGraph)
        {
            matrix = new DiGraph(VertexPrefab, DiEdgePrefab, OriginalMaterial, MarkedMaterial);
        }
        else
        {
            matrix = new Graph(VertexPrefab, EdgePrefab, OriginalMaterial, MarkedMaterial);
        }
        
        matrix.construct.Construct(edgesCopy);
        Print();
    }

    public void GraphColor()
    {

        ConstructDiGraph();
        Print();
        return;


        if (matrix.IsConsistent())
        {
            if (DiGraph)
            {
                ConstructDiGraph();
                return;
            }
            if (matrix.markModule.IsTwoColored())
            {
                ConstructDiGraph();
                //GetComponent<FlowAlgorithm>().Check(1);
                //GetComponent<FlowAlgorithm>().Algorithm(10000000);
                //infoText.text = "Maksymalne skojarzenie : " + ((DiGraph)matrix).GetAssociation();
                Print();
                return;
            }
        }
    }

    private OList<MathEdgeStruct> GetEdges()
    {
        OList<MathEdgeStruct> edgesCopy = new OList<MathEdgeStruct>();

        OList<EdgeObject> edges = matrix.locationModule.GetEdges();
        for(int i=0; i< edges.Count; i++)
        {
            edgesCopy.Add(edges[i].Copy());
        }

        return edgesCopy;
    }
}
