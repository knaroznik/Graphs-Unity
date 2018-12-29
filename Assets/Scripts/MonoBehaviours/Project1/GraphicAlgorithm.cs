using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphicAlgorithm : BaseAlgorithm {

    private string graphicList;

    public void GraphicGraphChanged(string _name)
    {
        graphicList = _name;
    }

    public override void Algorithm()
    {
        MatrixBehaviour mb = GetComponent<MatrixBehaviour>();

        if (graphicList == "" || graphicList == null)
        {

            return;
        }
        List<int> graph = graphicList.Split(',').Select(Int32.Parse).ToList();
        string output = "";
        if (sequenceIsGraphic(graph))
        {
            output += "Is Graphic\n";
            MathNeighborhoodMatrix mat = new MathNeighborhoodMatrix();
            mat.Construct(graph);
            output += mat.Print();
        }
        else
        {
            output += "Not graphic\n";
        }
        mb.infoText.text = output;
    }

    private bool sequenceIsGraphic(List<int> sequence)
    {
        var temp = new List<int>(sequence);
        int count = temp[0];
        if (count >= sequence.Count)
        {
            return false;
        }
        temp.RemoveAt(0);
        temp = temp.OrderByDescending(i => i).ToList();
        for (int i = 0; i < count; i++)
        {
            temp[i] -= 1;
        }

        if (temp.TrueForAll(s => s == 0))
        {
            return true;
        }

        if (temp.Exists(s => s < 0))
        {
            return false;
        }
        return sequenceIsGraphic(temp);
    }
}
