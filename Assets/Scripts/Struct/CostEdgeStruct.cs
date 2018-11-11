using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostEdgeStruct : EdgeStruct {

	protected int edgeCost;

	public CostEdgeStruct(Vertex _firstPoint, Vertex _secondPoint, int _edgeCost) : base(_firstPoint, _secondPoint){
		edgeCost = _edgeCost;
	}

	public int EdgeCost{
		get{
			return edgeCost;
		}
	}
}
