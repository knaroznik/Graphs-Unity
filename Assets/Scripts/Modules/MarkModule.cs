using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkModule {

    private Graph graph;

	public MarkModule(Graph _graph)
    {
        graph = _graph;
    }

    public void Mark(Vertex _toMark, Vertex prevVertex)
    {
        //Wiercholek z którego cechujemy
        _toMark.pathVertex = prevVertex;
        //Znalezienie ile można przenieść 
        if(prevVertex == null)
        {
            _toMark.pathCost = Mathf.Infinity;
        }
        else
        {
            float prevCost = prevVertex.pathCost;
            EdgeObject currentCost = graph.GetEdge(prevVertex, _toMark);
            _toMark.pathCost = Mathf.Min(prevCost, currentCost.EdgeCost);
            _toMark.sign = currentCost.Sign;
        }
    }

    public void ResetMarks()
    {
        for(int i=0; i<graph.vertexes.Count; i++)
        {
            graph.vertexes[i].Reset();
        }
    }

}
