using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C3Algorithm : BaseAlgorithm {

    public override void Algorithm()
    {
        MatrixBehaviour mb = GetComponent<MatrixBehaviour>();
        Graph graph = mb.matrix;
        mb.infoText.text = graph.cycleModule.CheckCycles();
    }
}
