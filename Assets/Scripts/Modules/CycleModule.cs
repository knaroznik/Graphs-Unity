using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleModule {

    private Graph graph;

    public CycleModule(Graph _graph)
    {
        graph = _graph;
    }

    public string CheckCycles()
    {
        string output = graph.Print();
        output += naiveCycles();
        output += findCycleMultiplication();
        return output;
    }

    private string naiveCycles()
    {
        int cyclesFound = 0;

        for (int i = 0; i < graph.vertexes.Count; i++)
        {
            findCycleLength(i, new OList<Vertex>(), i, 3, ref cyclesFound);
        }


        if (cyclesFound > 0)
        {
            return "\n\nNaive C3 : YES";
        }
        else
        {
            return "\n\nNaive C3 : NO";
        }
    }

    private bool findCycleLength(int current, OList<Vertex> visited, int original, int lenght, ref int cyclesFound)
    {
        if (expectedLength(visited.Count, lenght-1))
        {
            if (connected(original, current))
            {
                cyclesFound++;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            visited.Add(graph.vertexes[current]);
            for (int i = 0; i < graph.vertexes.Count; i++)
            {
                if (connected(current, i) && !visited.ToList().Contains(graph.vertexes[i]))
                {
                        findCycleLength(i, new OList<Vertex>(visited.ToList()), original, lenght, ref cyclesFound);
                }
            }
        }

        return false;
    }

    private bool expectedLength(int visitedVertexes, int expectedVertexes)
    {
        if(visitedVertexes == expectedVertexes)
        {
            return true;
        }
        return false;
    }

    private bool connected(int original, int current)
    {
        if(graph.vertexes[original][current] == 1)
        {
            return true;
        }
        return false;
    }

    private string findCycleMultiplication()
    {
        var matrix = vertexesToArray();
        var matrixN = matrix;
        for (int i = 0; i < 2; i++)
        {
            matrixN = Utilities.MultiplyMatrix(matrixN, matrix);
        }

        var trace = Utilities.MatrixTrace(matrixN);

        if (trace / 6 > 0)
        {
            return "\nMULTIPLE : YES";
        }
        else
        {
            return "\nMULTIPLE : NO";
        }
    }

    private int[][] vertexesToArray()
    {
        int[][] output = new int[graph.vertexes.Count][];
        for (int i = 0; i < graph.vertexes.Count; i++)
        {
            output[i] = new int[graph.vertexes.Count];
            for (int j = 0; j < graph.vertexes.Count; j++)
            {
                output[i][j] = graph.vertexes[i][j];
            }
        }
        return output;
    }

}
