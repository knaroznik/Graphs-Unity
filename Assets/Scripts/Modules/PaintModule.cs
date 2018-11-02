using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintModule {

	private Material originalMaterial;
	private Material markedMaterial;

	private OList<GameObject> markedObjects;

	public PaintModule(Material _originalMaterial, Material _markedMaterial){
		originalMaterial = _originalMaterial;
		markedMaterial = _markedMaterial;
		markedObjects = new OList<GameObject> ();
	}

	public void Paint(OList<Vertex> vertexes){
		UnPaint ();

		for (int i = 0; i < vertexes.Count; i++) {
			markedObjects.Add (vertexes [i].VertexObject);
			vertexes [i].VertexObject.GetComponent<Renderer> ().material = markedMaterial;
		}
	}

	private void UnPaint(){
		for (int i = 0; i < markedObjects.Count; i++) {
			markedObjects [i].GetComponent<Renderer> ().material = originalMaterial;
		}
		markedObjects = new OList<GameObject> ();
	}

	public void Reset(){
		UnPaint ();
	}
}
