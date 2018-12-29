using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class MatrixBehaviour : MonoBehaviour {

	public GameObject VertexPrefab;
	public GameObject EdgePrefab;
	public Text infoText;

	public Graph matrix;

	public bool DebugMode;
	public bool DiGraph;

	[Header("Paint materials")]
	public Material OriginalMaterial;
	public Material MarkedMaterial;

	void Awake () {
		if (DiGraph) {
			matrix = new DiGraph (VertexPrefab, EdgePrefab, OriginalMaterial, MarkedMaterial);
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
}
