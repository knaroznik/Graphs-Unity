using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiEdgeObject : EdgeObject {

    OList<LineRenderer> renderers;

    public override bool IsSame(EdgeObject edge)
    {
        if (edge.obj1 == obj1 && edge.obj2 == obj2)
        {
            return true;
        }

        return false;
    }

    public override bool IsSame(VertexObject one, VertexObject two)
    {
        if (one == obj1 && two == obj2)
        {
            return true;
        }

        return false;
    }

    public override void Init(VertexObject one, VertexObject two, int _edgeCost)
    {
        base.Init(one, two, _edgeCost);

        renderers = new OList<LineRenderer>();
        LineRenderer[] tempRenderers = GetComponentsInChildren<LineRenderer>();
        for(int i=0; i<tempRenderers.Length; i++)
        {
            renderers.Add(tempRenderers[i]);
        }
    }

    protected override void updatePosition()
    {

        Vector3 centerPoint = (obj1.transform.position + obj2.transform.position) / 2;

        if (obj1 != null && obj2 != null)
        {
            if (!isLoop)
            {
                renderers[0].SetPosition(0, obj1.transform.position);
                renderers[0].SetPosition(1, centerPoint);
                renderers[1].SetPosition(0, centerPoint);
                renderers[1].SetPosition(1, obj2.transform.position);
            }
            else
            {
                //TODO : Jeśli pętla to robić inaczej.
            }
        }
    }
}
