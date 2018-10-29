using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NeighborhoodMatrix{

	private OList<Vertex> vertexes;
	private GameObject vertexPrefab;
	private GameObject edgePrefab;

	public NeighborhoodMatrix(GameObject _vertexPrefab, GameObject _edgePrefab){
		vertexes = new OList<Vertex> ();
		vertexPrefab =  _vertexPrefab;
		edgePrefab = _edgePrefab;
	}

	public void AddVertex(string _newVertexName){

		if (vertexes.IndexOf (new Vertex (_newVertexName)) != -1) {
			return;
		}

		GameObject vertex = MonoBehaviour.Instantiate (vertexPrefab, new Vector3 (Random.Range (-7f, 7f), Random.Range (-7f, 7f), 0f), Quaternion.identity);
		for (int i = 0; i < vertexes.Count; i++) {
			vertexes.Get (i).AddPossibility (vertex);
		}

		vertexes.Add (new Vertex (vertex, _newVertexName));

		vertexes.Get (vertexes.Count - 1).AddPossibilities (vertexes.Count, vertexes);
	}

	public void RemoveVertex(string _vertex){
		int vertexNumber = vertexes.IndexOf (new Vertex (_vertex));
		
		for (int i = 0; i < vertexes.Count; i++) {
			vertexes [i].RemoveAt (vertexNumber);
		}


		vertexes [vertexNumber].VertexObject.GetComponent<VertexObject> ().Destroy ();
		vertexes.RemoveAt (vertexNumber);
	}

	public void AddEdge(int x, int y){
		vertexes [x] [y] += 1;
		vertexes [y] [x] += 1;
	}

	public void AddEdge(string one, string two){
		int x = vertexes.IndexOf (new Vertex (one));
		int y = vertexes.IndexOf (new Vertex (two));
		if(x == -1 || y == -1)
			return;
		AddEdge (x, y);

		GameObject line = MonoBehaviour.Instantiate (edgePrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		line.GetComponent<EdgeObject> ().Init (vertexes.Get (x).VertexObject.GetComponent<VertexObject>(), vertexes.Get (y).VertexObject.GetComponent<VertexObject>());
	}

	public void RemoveEdge(int x, int y){
		vertexes [x] [y] -= 1;
		vertexes [y] [x] -= 1;
	}

	public void RemoveEdge(string one, string two){
		int x = vertexes.IndexOf (new Vertex (one));
		int y = vertexes.IndexOf (new Vertex (two));
		if(x == -1 || y == -1)
			return;
		RemoveEdge (x, y);
		vertexes.Get (x).VertexObject.GetComponent<VertexObject> ().RemoveEdgeWith (vertexes.Get (y).VertexObject.GetComponent<VertexObject> ());
	}

	public string Print(){
		string output = "";
		output += " Macierz sąsiedztwa : \n";
		for (int i = 0; i < vertexes.Count; i++)
		{
			output += "\t" + vertexes[i].VertexName;
		}

		for (int i = 0; i < vertexes.Count; i++)
		{
			output += "\n";
			for (int j = 0; j < vertexes.Get(i).Count; j++)
			{
				if(j == 0)
				{
					output += vertexes[i].VertexName;
				}

				output += "\t" + vertexes[i][j];                  
			}
		}

		output += "\n\nNajwyższy stopien : " + HeighestValue();
		output += "\nNajniższy stopien : " + LowestValue();
		output += "\nParzystych stopni : " + EvenValues();
		output += "\nNieparzystych stopien : " + OddValues();
		output += "\nCiąg : " + VertexArray();
		return output;
	}

	public string HeighestValue(){
		int max = 0;
		for(int i=0; i<vertexes.Count; i++){
			int x = vertexes[i].Value();
			if(x > max)
				max = x;
		}
		return max.ToString();
	}

	public string LowestValue(){
		int min = 999;
		for(int i=0; i<vertexes.Count; i++){
			int x = vertexes[i].Value();
			if(x < min)
				min = x;
		}
		return min.ToString();
	}

	public string EvenValues(){
		int x = 0;
		for(int i=0; i<vertexes.Count; i++){
			if(vertexes[i].Value()%2 == 0)
				x++;
		}
		return x.ToString();
	}

	public string OddValues(){
		int x = 0;
		for(int i=0; i<vertexes.Count; i++){
			if(vertexes[i].Value()%2 == 1)
				x++;
		}
		return x.ToString();
	}

	public string VertexArray(){
		List<int> x =new List<int>();
		
		for(int i=0; i<vertexes.Count; i++){
			x.Add(vertexes[i].Value());
		}
		x.Sort();
		x.Reverse();
		string q = "";
		for(int i=0; i<x.Count; i++){
			q+=x[i] + ",";
		}
		return q;
	}


	public string CheckCycles(){
		string output = Print ();
		output += naiveCycles ();
		output += findCycleMultiplication ();
		return output;
	}

	private string naiveCycles(){
		List<int> visitDict = new List<int>();
		int cyclesFound = 0;
		for (int i = 0; i < vertexes.Count; i++)
		{
			findCycle(i, visitDict, -1, ref cyclesFound);
		}

		if(cyclesFound > 0)
		{
			return "\n\nNaive C3 : YES";
		}
		else
		{
			return "\n\nNaive C3 : NO";
		}
	}

	private string findCycleMultiplication()
	{
		var matrix = VertexesToArray ();
		var matrixN = matrix;
		for (int i = 0; i < 2; i++)
		{
			matrixN = Utilities.MultiplyMatrix(matrixN, matrix);
		}

		var trace = Utilities.MatrixTrace(matrixN);

		if (trace/6 > 0)
		{
			return "\nMULTIPLE : YES";
		}
		else
		{
			return "\nMULTIPLE : NO";
		}
	}

	private int[][] VertexesToArray(){
		int[][] output = new int[vertexes.Count][];
		for (int i = 0; i < vertexes.Count; i++) {
			output [i] = new int[vertexes.Count];
			for (int j = 0; j < vertexes.Count; j++) {
				output [i] [j] = vertexes [i] [j];
			}
		}
		return output;
	}

	private bool findCycle(int current, List<int> visited, int parent, ref int cyclesFound)
	{
		visited.Add(current);
		for (int i = 0; i < vertexes[current].Count; i++)
		{
			if (vertexes[current][i] == 1)
			{
				if (!visited.Contains(i))
				{
					findCycle(i, visited, current, ref cyclesFound);
				}
				else
				{
					if(parent < 0)
						continue;

					if (i != parent && vertexes[i][parent] == 1)
					{
						cyclesFound++;
						return true;
					}
				}
			}
		}

		return false;
	}
}
