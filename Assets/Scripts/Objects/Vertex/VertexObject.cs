using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexObject : VertexBaseObject {

	private Vector3 screenPoint;
	private Vector3 offset;

	private OList<EdgeObject> edges;

	public Text textName;
	public Vertex vertexData;

	void Awake(){
		edges = new OList<EdgeObject> ();
	}

    public void UpdateEdges()
    {
        for(int i=0; i<edges.Count; i++)
        {
            edges[i].UpdateEdge();
        }
    }

	public void SetName(string _name, Vertex _vertexData){
		textName.text = _name;
		vertexData = _vertexData;
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
				return;
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

	public int EdgeCount{
		get{
			return edges.Count;
		}
	}

	public EdgeObject Edge(int i){
		return edges [i];
	}

    public int Value
    {
        get
        {
            return GetValue();
        }
    }

    private int GetValue()
    {
        int value = 0;
        for(int i=0; i<EdgeCount; i++)
        {
            value += Edge(i).EdgeCost;
        }
        return value;
    }

    private void OnMouseOver()
    {
        InputBehaviour.instance.CurrentSelectedGameObject = this.gameObject;
    }

    private void OnMouseExit()
    {
        InputBehaviour.instance.CurrentSelectedGameObject = null;
    }
}
