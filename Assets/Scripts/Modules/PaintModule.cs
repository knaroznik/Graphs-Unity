using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintModule {

	private Material originalMaterial;
	private Material markedMaterial;

	private OList<GameObject> markedObjects;
	private OList<Color> colors;

	public PaintModule(Material _originalMaterial, Material _markedMaterial){
		originalMaterial = _originalMaterial;
		markedMaterial = _markedMaterial;
		markedObjects = new OList<GameObject> ();
		initColors ();
	}

	private void initColors(){
		colors = new OList<Color> ();
		colors.Add (Color.blue);
		colors.Add (Color.cyan);
		colors.Add (Color.gray);
		colors.Add (Color.green);
		colors.Add (Color.magenta);
		colors.Add (Color.red);
		colors.Add (Color.yellow);
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

	public void Paint(OList<OList<Vertex>> _vertexes){
		markedObjects = new OList<GameObject> ();
		for (int i = 0; i < _vertexes.Count; i++) {
			OList<Vertex> part = _vertexes [i];

			for (int j = 0; j < part.Count; j++) {
				markedObjects.Add (part [j].VertexObject);

				part [j].VertexObject.GetComponent<Renderer> ().material.color = colors [i];
			}
		}
	}
}
