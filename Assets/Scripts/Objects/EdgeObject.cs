using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeObject : MonoBehaviour {

	private VertexObject obj1;
	private VertexObject obj2;
	private LineRenderer render;

	private bool isLoop = false;

	public void Init(VertexObject one, VertexObject two){
		obj1 = one;
		obj2 = two;
		
		render = this.GetComponent<LineRenderer> ();
		if(obj1 == obj2){
			obj1.AddEdge (this);
			isLoop = true;
		}else{
			obj1.AddEdge (this);
			obj2.AddEdge (this);
		}

	}

	void Update(){
		if (obj1 != null && obj2 != null) {
			if(!isLoop){
				render.SetPosition (0, obj1.transform.position);
				render.SetPosition (1, obj2.transform.position);
			}else{
				render.positionCount = 4;
				render.SetPosition (0, obj1.transform.position);
				render.SetPosition (1, obj1.transform.position + new Vector3(-1, 1, 0));
				render.SetPosition (2, obj1.transform.position + new Vector3(1, 1, 0));
				render.SetPosition (3, obj1.transform.position);
			}
		}
	}

	public bool IsSame(VertexObject one, VertexObject two){
		if (one == obj1 && two == obj2) {
			return true;
		}

		if (two == obj1 && one == obj2) {
			return true;
		}

		return false;
	}

	public void Destroy(){
		if(!isLoop){
		obj1.RemoveEdge (this);
		obj2.RemoveEdge (this);
		}else{
			obj1.RemoveEdge (this);
		}
		Destroy (this.gameObject);
	}
}
