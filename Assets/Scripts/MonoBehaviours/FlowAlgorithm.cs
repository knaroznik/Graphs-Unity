using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowAlgorithm : MonoBehaviour {

    private Text sceneText;
    MatrixBehaviour matrixBehaviour;
    DiGraph graph;

    Vertex source;
    Vertex escape;

    public void Check()
    {
        Init();
        graph = (DiGraph)matrixBehaviour.matrix;


        OList<Vertex> sources = graph.GetSources();
        OList<Vertex> escapes = graph.GetEscapes();

        if (sources.Count == 0 || escapes.Count == 0)
        {
            CrashAlgorithm();
            return;
        }
        else
        {
            GetSource(sources);
            GetEscape(escapes);

        }

        if (!Connected())
        {
            CrashAlgorithm();
            return;
        }
    }

    public void Check(int _value)
    {
        Init();
        graph = (DiGraph)matrixBehaviour.matrix;


        OList<Vertex> sources = graph.GetSources();
        OList<Vertex> escapes = graph.GetEscapes();

        if (sources.Count == 0 || escapes.Count == 0)
        {
            CrashAlgorithm();
            return;
        }
        else
        {
            GetSource(sources, _value);
            GetEscape(escapes, _value);

        }

        if (!Connected())
        {
            CrashAlgorithm();
            return;
        }
    }

    public void Algorithm(int steps = 1)
    {
        DiGraph graph = (DiGraph)matrixBehaviour.matrix;
        int whileCounter = 0;
        

        while (whileCounter < steps)
        {
            //Mark Phase

            //GetBorderes Init 

            Vertex currentVertex = source;
            Queue<Vertex> queue = new Queue<Vertex>();
            queue.Add(currentVertex);

            //Ocechowanie S
            graph.markModule.Mark(currentVertex, null);

            while (!queue.Contains(escape))
            {
                int markedVertexes = 0;
                OList<Vertex> borderers = graph.FindBorderers(currentVertex);
                string x = "";
                for (int i = 0; i < borderers.Count; i++)
                {
                    //Ocechuj jeśli nie ma w kolejce
                    if (!queue.Contains(borderers[i]))
                    {
                        //Cechuj borderers[i] poprzez currentVertex - pamiętać o krawędzi pomiędzy jednym a drugim
                        graph.markModule.Mark(borderers[i], currentVertex);
                        //Dodaj borderers[i] do kolejki
                        queue.Add(borderers[i]);
                        x += borderers[i].VertexName;
                        markedVertexes++;
                    }
                }
                //Jeśli nie ma gdzie dalej pójść - koniec algorytmu - obecna kolejka idzie do zakończenia algorytmu
                if (markedVertexes == 0 && queue.isEmpty())
                {
                    Quit(queue);
                    return;
                }

                //Zmiana aktualnego wierzchołka
                queue.Remove();
                currentVertex = queue.GetFirstItem();
            }


            //RETURN PHASE
            //Od wierzcholka T funkcja rekurencyjna aż do S.
            sceneText.text = "Znaleziona ścieżka " + escape.Return(graph, escape.pathCost);
            //Resetowanie cechowania
            graph.markModule.ResetMarks();

            whileCounter++;
        }

        
    }

    private void Init()
    {
        matrixBehaviour = GetComponent<MatrixBehaviour>();
        sceneText = matrixBehaviour.infoText;
    }

    private void GetSource(OList<Vertex> _sources, int _value = -1)
    {
        if (_sources.Count > 1)
        {
            matrixBehaviour.matrix.AddNewVertex(new Vector3(-18, 0, 0), "S");

            for (int i = 0; i < _sources.Count; i++)
            {
                if(_value != -1)
                {
                    matrixBehaviour.matrix.AddEdge("S", _sources[i].VertexName, _value);
                }
                else
                {
                    matrixBehaviour.matrix.AddEdge("S", _sources[i].VertexName, _sources[i].Value);
                }
                
            }
            int vertexNumber = matrixBehaviour.matrix.vertexes.IndexOf(new Vertex("S"));
            source = matrixBehaviour.matrix.vertexes[vertexNumber];
        }
        else
        {
            source = _sources[0];
        }
    }

    private void GetEscape(OList<Vertex> _escapes, int _value = -1)
    {
        if (_escapes.Count > 1)
        {
            matrixBehaviour.matrix.AddNewVertex(new Vector3(6, 0, 0), "T");

            for (int i = 0; i < _escapes.Count; i++)
            {
                if(_value != -1)
                {
                    matrixBehaviour.matrix.AddEdge(_escapes[i].VertexName, "T", _value);
                }
                else
                {
                    matrixBehaviour.matrix.AddEdge(_escapes[i].VertexName, "T", _escapes[i].Value);
                }
                
            }
            int vertexNumber = matrixBehaviour.matrix.vertexes.IndexOf(new Vertex("T"));
            escape = matrixBehaviour.matrix.vertexes[vertexNumber];
        }
        else
        {
            escape = _escapes[0];
        }
    }

    private bool Connected()
    {
        List<Vertex> connectedVertexes = matrixBehaviour.matrix.GetConnectedVertexes(source.VertexName);

        if (!connectedVertexes.Contains(escape))
        {
            return false;
        }

        return true;
    }

    private void Quit(Queue<Vertex> endQueue)
    {
        string output = "";
        OList<Vertex> v = endQueue.GetQueue();

        Area<Vertex> area1 = new Area<Vertex>(v);
        Area<Vertex> area2 = new Area<Vertex>();
        area2.CreateArea(matrixBehaviour.matrix.vertexes, endQueue);
        output += "Podział : " + area1.ToString() + " , " + area2.ToString() + "\n";

        //Wyciągnij z kolejki Maksymalny przepływ i Minimalny przekrój.
        OList<EdgeObject> minimalnyPrzekroj = graph.FindEdgesBetweenAreas(area1, area2);
        output += "Minimalny przekrój : " + minimalnyPrzekroj.ToString() + "\n";
        output += "Maksymalny przepływ : " + graph.GetEdgesValue(minimalnyPrzekroj);

        

        sceneText.text = output;
    }

    private void CrashAlgorithm()
    {
        sceneText.text = "Input correct digraph! :(";
    }
}
