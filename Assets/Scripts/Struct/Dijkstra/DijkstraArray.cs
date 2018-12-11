using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraArray {

    OList<DijkstraStruct> array;

    public DijkstraArray(OList<Vertex> _vertexes)
    {
        array = new OList<DijkstraStruct>();
        for(int i=0; i<_vertexes.Count; i++)
        {
            array.Add(new DijkstraStruct(_vertexes[i].VertexName));
        }
    }

    public void CheckValue(DijkstraStruct _destinationVertex, DijkstraStruct _currentVertex, int _edgeCost)
    {
        if(_destinationVertex.PathCost > _currentVertex.PathCost + _edgeCost)
        {
            _destinationVertex.PathCost = _currentVertex.PathCost + _edgeCost;
            _destinationVertex.Path = new OList<string>();
            for(int i=0; i< _currentVertex.Path.Count; i++)
            {
                _destinationVertex.Path.Add(_currentVertex.Path[i]);
            }
            _destinationVertex.Path.Add(_currentVertex.VertexName);
        }
    }

    public string Min(OList<Vertex> uncheckedVertexes)
    {
        string output = "";
        float minValue = Mathf.Infinity;
        for(int i=0; i<array.Count; i++)
        {
            if(array[i].PathCost < minValue && UnSeenVertex(array[i].VertexName, uncheckedVertexes))
            {
                output = array[i].VertexName;
                minValue = array[i].PathCost;
            }
        }
        return output;
    }

    public bool UnSeenVertex(string currentVertex, OList<Vertex> uncheckedVertexes)
    {
        for(int i=0; i<uncheckedVertexes.Count; i++)
        {
            if(uncheckedVertexes[i].VertexName == currentVertex)
            {
                return true;
            }
        }
        return false;
    }

    public DijkstraStruct Get(string _name)
    {
        for(int i=0; i<array.Count; i++)
        {
            if(array[i].VertexName == _name)
            {
                return array[i];
            }
        }
        return null;
    }

    public DijkstraStruct Get(int _number)
    {
        for (int i = 0; i < array.Count; i++)
        {
            if (i == _number)
            {
                return array[i];
            }
        }
        return null;
    }

    public int Length()
    {
        return array.Count;
    }
}
