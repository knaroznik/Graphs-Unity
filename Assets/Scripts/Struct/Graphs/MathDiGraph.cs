using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathDiGraph : MathNeighborhoodMatrix{


	public void Transpond(){
		OList<Vertex> copy = new OList<Vertex> ();
        for (int i = 0; i < vertexes.Count; i++){
            copy.Add(new Vertex(vertexes[i].VertexName, vertexes[i].connections));
        }

        vertexes.Clear();

        for(int i=0; i< copy.Count; i++)
        {
            AddVertex(copy[i].VertexName);
        }

        for (int i=0; i<copy.Count; i++)
        {
            for(int j=0; j< copy[i].connections.Count; j++)
            {
                if(copy[i][j] == 1)
                {
                    vertexes[j][i] += 1;
                }
            }
        }
	}

	private void RemoveVertexes(OList<Vertex> _toRemove){
		for (int i = 0; i < _toRemove.Count; i++) {
            RemoveNamedVertex(_toRemove[i].VertexName);

        }
	}

    public override void AddEdge(int x, int y)
    {
        vertexes[x][y] += 1;
    }

    public override void RemoveEdge(int x, int y)
    {
        vertexes[x][y] -= 1;
    }

    public OList<Vertex> GetConsistencyPartOf(Vertex start){
		
		Vertex currentVertex = start;
		Stack<Vertex> stack = new Stack<Vertex> ();
		OList<Vertex> visitedVertexes = new OList<Vertex> ();
		stack.Push (currentVertex);
		visitedVertexes.Add (currentVertex);

		while (!stack.IsEmpty ()) {
			OList<Vertex> borderers = FindUnseenBorderers (currentVertex, visitedVertexes);
			if (borderers.Count > 0) {
				currentVertex = visitVertex (borderers [0], ref stack, ref visitedVertexes);
			} else {
				stack.Pop ();
				if(!stack.IsEmpty())
					currentVertex = stack.Last ();
			}
		}


		RemoveVertexes (visitedVertexes);
		return visitedVertexes;
	}

	private OList<Vertex> FindUnseenBorderers(Vertex _currentVertex, OList<Vertex> _visited){
		OList<Vertex> output = new OList<Vertex> ();
        int vertexNumber = vertexes.IndexOf(new Vertex(_currentVertex.VertexName));
        for (int i = 0; i < vertexes[vertexNumber].connections.Count; i++) {
			if (vertexes[vertexNumber].connections[i] == 1) {
				if (!_visited.ToList ().Contains (vertexes [i])) {
					output.Add (vertexes[i]);
				}
			}
		}
		return output;
	}

	protected Vertex visitVertex(Vertex addVertex, ref Stack<Vertex> _stack, ref OList<Vertex> _visitedVertexes){
		_stack.Push (addVertex);
		_visitedVertexes.Add (addVertex);
		return addVertex;
	}

}
