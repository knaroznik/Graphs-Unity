using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiConstructModule : ConstructModule {

    public DiConstructModule(GameObject _vertexPrefab, GameObject _edgePrefab, PaintModule _brush, Graph _graph) : base(_vertexPrefab, _edgePrefab, _brush, _graph)
    {
    }

    public override void AddEdge(int x, int y)
    {
        if (brush != null)
        {
            brush.Reset();
        }
        graph.vertexes[x][y] += 1;
        graph.vertexes[x].OutEdges++;
        graph.vertexes[y].InEdges++;
    }

    public override void RemoveEdge(int x, int y)
    {
        if (brush != null)
        {
            brush.Reset();
        }

        graph.vertexes[x][y] -= 1;
        graph.vertexes[x].OutEdges--;
        graph.vertexes[y].InEdges--;
    }
}
