using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiConstructModule : ConstructModule {

    public DiConstructModule(GameObject _vertexPrefab, GameObject _edgePrefab, PaintModule _brush) : base(_vertexPrefab, _edgePrefab, _brush)
    {
    }

    public override void AddEdge(int x, int y, ref OList<Vertex> _vertexes)
    {
        if (brush != null)
        {
            brush.Reset();
        }
        _vertexes[x][y] += 1;
    }

    public override void RemoveEdge(int x, int y, ref OList<Vertex> _vertexes)
    {
        if (brush != null)
        {
            brush.Reset();
        }
        _vertexes[x][y] -= 1;
    }
}
