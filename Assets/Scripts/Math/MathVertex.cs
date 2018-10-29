using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathVertex {

	private OList<int> connections;

	public MathVertex(){
		connections = new OList<int> ();
	}

	public void AddPossibility(){
		connections.Add (0);
	}

	public void AddPossibilities(int howMuch){
		for (int i = 0; i < howMuch; i++) {
			connections.Add (0);
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
	}

	public int Value(){
		int x = 0;
		for(int i=0; i<connections.Count; i++){
			x+=connections[i];

		}
		return x;
	}
}
