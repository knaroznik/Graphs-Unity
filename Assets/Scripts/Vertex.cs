using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex {

	private OList<int> connections;
	private OList<GameObject> connectionObjects;

	private GameObject vertexObject;
	private string vertexName;

	public GameObject VertexObject{
		get{
			return vertexObject;
		}

		set{
			vertexObject = value;
		}
	}

	public string VertexName{
		get{
			return vertexName;
		}
	}

	public override bool Equals (object obj)
	{
		
		if (obj.GetType () == this.GetType ()) {
			Vertex x = (Vertex)obj;
			if (x.VertexName.Equals(this.VertexName)) {
				return true;
			}
		}
		return false;
	}

	public Vertex(GameObject _vertexObject, string _vertexName){
		connections = new OList<int> ();
		connectionObjects = new OList<GameObject> ();
		VertexObject = _vertexObject;
		vertexName = _vertexName;

		VertexObject.GetComponent<VertexObject> ().SetName (_vertexName);
	}

	public Vertex(string _vertexName){
		vertexName = _vertexName;
	}

	public void AddPossibility(GameObject _newVertex){
		connections.Add (0);
		connectionObjects.Add (_newVertex);
	}

	public void AddPossibilities(int howMuch, OList<Vertex> _vertexes){
		for (int i = 0; i < howMuch; i++) {
			connections.Add (0);
			connectionObjects.Add (_vertexes [i].VertexObject);
		}
	}

	public int Count{
		get{
			return connections.Count;
		}
	}

	public int this[int index]{
		get{
			return connections.Get (index);
		}
		set{
			connections.Insert (value, index);
		}
	}

	public void RemoveAt(int i){
		connections.RemoveAt (i);
		connectionObjects.RemoveAt (i);
	}

	public int Value(){
		int x = 0;
		for(int i=0; i<connections.Count; i++){
			x+=connections[i];
			
		}
		return x;
	}
}
