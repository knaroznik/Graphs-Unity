using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeStruct {

	private Vertex firstPoint, secondPoint;

	public EdgeStruct(Vertex _firstPoint, Vertex _secondPoint){
		firstPoint = _firstPoint;
		secondPoint = _secondPoint;
	}

	public Vertex FirstPoint(){
		return firstPoint;
	}

	public Vertex SecondPoint(){
		return secondPoint;
	}

	public override string ToString ()
	{
		return firstPoint.ToString() + " | " + secondPoint.ToString();

	}
}
