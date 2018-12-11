using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DijkstraAlgorithm : MonoBehaviour {

    private Text sceneText;
    private OList<Vertex> vertexes;
    private OList<EdgeObject> edges;
    private MatrixBehaviour matrixBehaviour;

    public string startingVertex;

    void Construct()
    {
        matrixBehaviour = GetComponent<MatrixBehaviour>();
        vertexes = matrixBehaviour.matrix.vertexes;
        edges = matrixBehaviour.matrix.GetEdges();
        sceneText = matrixBehaviour.infoText;
    }

    public void Algorithm()
    {
        Construct();

        if (!matrixBehaviour.matrix.IsConsistent())
        {
            sceneText.text = "Not consistent!";
            return;
        }

        string currentVertex = startingVertex;
        DijkstraArray array = new DijkstraArray(vertexes);
        array.Get(currentVertex).PathCost = 0;
        OList<Vertex> uncheckedVertexes = vertexes.Copy();

        uncheckedVertexes.Remove(new Vertex(currentVertex));

        for(int i=0; i< uncheckedVertexes.Count; i++)
        {
            EdgeObject edge = GetEdge(currentVertex, uncheckedVertexes[i].VertexName);
            if (edge != null)
            {
                array.CheckValue(array.Get(uncheckedVertexes[i].VertexName), array.Get(currentVertex), edge.edgeCost);
            }
        }

        

        while (uncheckedVertexes.Count != 0)
        {
            currentVertex = array.Min(uncheckedVertexes);
            
            uncheckedVertexes.Remove(new Vertex(currentVertex));

            for (int i = 0; i < uncheckedVertexes.Count; i++)
            {
                EdgeObject edge = GetEdge(currentVertex, uncheckedVertexes[i].VertexName);
                if (edge != null)
                {
                    array.CheckValue(array.Get(uncheckedVertexes[i].VertexName), array.Get(currentVertex), edge.edgeCost);
                }
            }
        }

        sceneText.text = Print(array);
    }

    EdgeObject GetEdge(string _x, string _y)
    {
        for(int i=0; i<edges.Count; i++)
        {
            if(edges[i].obj1.vertexData.VertexName == _x && edges[i].obj2.vertexData.VertexName == _y)
            {
                return edges[i];
            }
        }
        return null;
    }

    string Print(DijkstraArray array)
    {
        string output = "";
        for(int i=0; i< array.Length(); i++)
        {
            output += array.Get(i).ToString();
        }
        return output;
    }
}
