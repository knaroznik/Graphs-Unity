using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathDiGraph {

	OList<Vertex> vertexes;
	OList<EdgeStruct> edges;

	public MathDiGraph(OList<Vertex> _originalVertexes, OList<EdgeObject> _originalEdges){
		vertexes = new OList<Vertex> ();
		for (int i = 0; i < _originalVertexes.Count; i++) {
			vertexes.Add (_originalVertexes [i]);
		}

		edges = new OList<EdgeStruct> ();

		for (int i = 0; i < _originalEdges.Count; i++) {
			edges.Add (new EdgeStruct (_originalEdges[i].obj1.vertexData, _originalEdges[i].obj2.vertexData));
		}
	}

	public void Transpond(){
		OList<EdgeStruct> copy = new OList<EdgeStruct> ();
		for (int i = 0; i < edges.Count; i++) {
			copy.Add (edges [i]);
		}
		edges = new OList<EdgeStruct> ();
		for (int i = 0; i < copy.Count; i++) {
			edges.Add (new EdgeStruct (copy [i].SecondPoint (), copy [i].FirstPoint ()));
		}
	}

	private void RemoveVertexes(OList<Vertex> _toRemove){
		for (int i = 0; i < _toRemove.Count; i++) {
			vertexes.Remove (_toRemove [i]);
			RemoveEdges (_toRemove [i]);
		}
	}

	private void RemoveEdges(Vertex _incidentalVertex){
		OList<EdgeStruct> toDelete = new OList<EdgeStruct> ();

		for (int i = 0; i < edges.Count; i++) {
			if (edges [i].FirstPoint () == _incidentalVertex || edges [i].SecondPoint () == _incidentalVertex) {
				toDelete.Add (edges [i]);
			}
		}

		for (int i = 0; i < toDelete.Count; i++) {
			edges.Remove (toDelete [i]);
		}
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

		for (int i = 0; i < edges.Count; i++) {
			if (edges [i].FirstPoint () == _currentVertex) {
				if (!_visited.ToList ().Contains (edges [i].SecondPoint ())) {
					output.Add (edges [i].SecondPoint ());
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
