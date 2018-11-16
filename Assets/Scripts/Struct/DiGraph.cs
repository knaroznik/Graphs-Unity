using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiGraph : Graph {
	
	public DiGraph(GameObject _vertexPrefab, GameObject _edgePrefab, Material _originalMaterial, Material _markedMaterial) : 
		base(_vertexPrefab, _edgePrefab, _originalMaterial, _markedMaterial){
		locationModule = new LocationDirectionModule ();
	}

	public Stack<Vertex> FullVertexesStack(){
		return ((LocationDirectionModule)locationModule).FullVertexesStack (this);
	}
}
