using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationDirectionModule : LocationModule {

    public LocationDirectionModule(Graph _graph) : base(_graph){}

    public Stack<Vertex> FullVertexesStack(DiGraph _matrix){

		Stack<Vertex> globalStack = new Stack<Vertex> ();
		OList<Vertex> globalVisited = new OList<Vertex> ();

		for (int i = 0; i < _matrix.vertexes.Count; i++) {

			if (globalVisited.ToList ().Contains (_matrix.vertexes [i])) {
				continue;
			}

			Vertex currentVertex;
			Stack<Vertex> stack = new Stack<Vertex> ();

			currentVertex = visitVertex (_matrix.vertexes [i], ref stack, ref globalVisited);

			while (!stack.IsEmpty ()) {

				OList<Vertex> borderers = FindUnseenBorderers (currentVertex, globalVisited, _matrix);
				if (borderers.Count > 0) {
					currentVertex = visitVertex (borderers [0], ref stack, ref globalVisited);
				} else {

					globalStack.Push (stack.Last ());
					globalVisited.Add(stack.Last());
					stack.Pop ();

					if (!stack.IsEmpty ())
						currentVertex = stack.Last ();
				}
			}
		}

		return globalStack;
	}

	private OList<Vertex> FindUnseenBorderers(Vertex _currentVertex, OList<Vertex> _visited, Graph _matrix){
		OList<Vertex> output = new OList<Vertex> ();
		for (int i = 0; i < _matrix.vertexes.Count; i++) {
			int current = _matrix.vertexes.IndexOf (new Vertex (_currentVertex.VertexName));
			if (IsConnected(_matrix.vertexes [current], _matrix.vertexes [i])) {
				if (!_visited.ToList ().Contains (_matrix.vertexes [i])) {
					output.Add (_matrix.vertexes [i]);
				}
			}
		}

		return output;
	}

	private bool IsConnected(Vertex _start, Vertex _end){
		int connections = _start.VertexObject.GetComponent<VertexObject> ().EdgeCount;
		VertexObject startObject = _start.VertexObject.GetComponent<VertexObject> ();
		for (int i = 0; i < connections; i++) {
			EdgeObject x = startObject.Edge (i);
			if (x.obj1 == startObject && x.obj2 == _end.VertexObject.GetComponent<VertexObject>()) {
				return true;
			}
		}
		return false;
	}
}
