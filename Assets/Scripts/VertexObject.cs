using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexObject : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;

	private OList<EdgeObject> edges;

	public Text textName;

	void Awake(){
		edges = new OList<EdgeObject> ();
	}

	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		offset =  transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,screenPoint.z));
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}

	public void SetName(string _name){
		textName.text = _name;
	}

	public void AddEdge(EdgeObject edge){
		edges.Add(edge);
	}

	public void RemoveEdge(EdgeObject edge){
		edges.Remove (edge);
	}

	public void RemoveEdgeWith(VertexObject anotherVertex){
		for (int i = 0; i < edges.Count; i++) {
			if (edges [i].IsSame (this, anotherVertex)) {
				EdgeObject x = edges [i];
				x.Destroy ();
			}
		}
	}

	public void Destroy(){
		for (int i = 0; i < edges.Count; i++) {
			edges [i].Destroy ();
			i--;
		}
		Destroy (this.gameObject);
	}

}
