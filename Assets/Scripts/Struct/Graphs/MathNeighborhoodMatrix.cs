using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathNeighborhoodMatrix {

	public OList<Vertex> vertexes;

	public MathNeighborhoodMatrix(){
		vertexes = new OList<Vertex> ();
	}

	public void AddVertex(){
		
		for (int i = 0; i < vertexes.Count; i++) {
			vertexes.Get (i).AddPossibility ();
		}

		vertexes.Add (new Vertex ("notImportant"));

		vertexes.Get (vertexes.Count - 1).AddPossibilities (vertexes.Count);
	}

    public void AddVertex(string _name)
    {

        for (int i = 0; i < vertexes.Count; i++)
        {
            vertexes.Get(i).AddPossibility();
        }

        vertexes.Add(new Vertex(_name));

        vertexes.Get(vertexes.Count - 1).AddPossibilities(vertexes.Count);
    }

    public void RemoveVertex(int _vertex){
		for (int i = 0; i < vertexes.Count; i++) {
			vertexes [i].RemoveAt (_vertex);
		}
			
		vertexes.RemoveAt (_vertex);
	}

	public virtual void AddEdge(int x, int y){
		vertexes [x] [y] += 1;
		vertexes [y] [x] += 1;
	}

	public virtual void RemoveEdge(int x, int y){
		vertexes [x] [y] -= 1;
		vertexes [y] [x] -= 1;
	}

	public string Print(){
		string output = "";
		output += " Macierz sąsiedztwa : \n";
		for (int i = 0; i < vertexes.Count; i++)
		{
			output += "\t" + i;
		}

		for (int i = 0; i < vertexes.Count; i++)
		{
			output += "\n";
			for (int j = 0; j < vertexes.Get(i).Count; j++)
			{
				if(j == 0)
				{
					output += i;
				}

				output += "\t" + vertexes[i][j];                  
			}
		}
		return output;
	}

	public void Construct(List<int> graph){
		for (int i = 0; i < graph.Count; i++)
		{
			AddVertex();
		}

		for (int i = 0; i < vertexes.Count; i++)
		{
			int degree = graph[0];
			var degreeOfVertex = vertexes [i].Value ();
			degree -= degreeOfVertex;
			for (int j = 1; j <= degree; j++)
			{
				for (int k = i+j; k < vertexes.Count; k++)
				{
					if (vertexes[i + j].Value() < graph[i + j])
					{
						AddEdge(i, i + j);
						break;
					}
				}
			}
		}
	}

	public void Construct(Graph m){
		vertexes = new OList<Vertex> ();
		for (int i = 0; i < m.vertexes.Count; i++) {
			vertexes.Add (new Vertex(m.vertexes[i].VertexName, m.vertexes[i].connections));
		}
	}

	public void RemoveNamedVertex(string _vertex){
		int vertexNumber = vertexes.IndexOf (new Vertex (_vertex));

		for (int i = 0; i < vertexes.Count; i++) {
			vertexes [i].RemoveAt (vertexNumber);
		}

		vertexes.RemoveAt (vertexNumber);
	}
}
