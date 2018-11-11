using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class NeighborhoodMatrix{

	public OList<Vertex> vertexes;
	private GameObject vertexPrefab;
	private GameObject edgePrefab;

	private PaintModule brush;
	private InfoModule info;
	private NucleusModule nucleus = new NucleusModule();
	private LocationModule locationModule = new LocationModule ();
	private ConstructModule construct = new ConstructModule();
	private ConsistencyModule consistency = new ConsistencyModule();

	public NeighborhoodMatrix(GameObject _vertexPrefab, GameObject _edgePrefab){
		vertexes = new OList<Vertex> ();
		vertexPrefab =  _vertexPrefab;
		edgePrefab = _edgePrefab;
		info = new InfoModule (this);
	}

	public NeighborhoodMatrix(GameObject _vertexPrefab, GameObject _edgePrefab, Material _originalMaterial, Material _markedMaterial){
		vertexes = new OList<Vertex> ();
		vertexPrefab =  _vertexPrefab;
		edgePrefab = _edgePrefab;
		info = new InfoModule (this);
		brush = new PaintModule (_originalMaterial, _markedMaterial);
	}

	public int Count{
		get{
			return vertexes.Count;
		}
	}

	public int LowestValue(){
		return info.LowestValue ();
	}

	public NucleusModule Nucleus{
		get{
			return nucleus;
		}
	}

	public void AddVertex(string _newVertexName){

		if (vertexes.IndexOf (new Vertex (_newVertexName)) != -1) {
			return;
		}

		if (brush != null) {
			brush.Reset ();
		}

		GameObject vertex = MonoBehaviour.Instantiate (vertexPrefab, new Vector3 (UnityEngine.Random.Range (-7f, 7f), UnityEngine.Random.Range (-7f, 7f), 0f), Quaternion.identity);
		for (int i = 0; i < vertexes.Count; i++) {
			vertexes.Get (i).AddPossibility (vertex);
		}

		vertexes.Add (new Vertex (vertex, _newVertexName));

		vertexes.Get (vertexes.Count - 1).AddPossibilities (vertexes.Count, vertexes);
	}

	public void RemoveVertex(string _vertex){
		int vertexNumber = vertexes.IndexOf (new Vertex (_vertex));

		if (vertexNumber == -1)
			return;

		
		for (int i = 0; i < vertexes.Count; i++) {
			vertexes [i].RemoveAt (vertexNumber);
		}
		if (brush != null) {
			brush.Reset ();
		}

		vertexes [vertexNumber].VertexObject.GetComponent<VertexObject> ().Destroy ();
		vertexes.RemoveAt (vertexNumber);
	}

	public void AddEdge(int x, int y){
		if (brush != null) {
			brush.Reset ();
		}
		vertexes [x] [y] += 1;
		vertexes [y] [x] += 1;
	}

	public void AddEdge(string one, string two, string edgeCost){
		int x = vertexes.IndexOf (new Vertex (one));
		int y = vertexes.IndexOf (new Vertex (two));
		int cost = 0;
		int.TryParse (edgeCost, out cost);

		if (edgeCost == "" || edgeCost == null) {
			cost = 1;
		}

		if(x == -1 || y == -1|| cost == 0)
			return;
		AddEdge (x, y);

		GameObject line = MonoBehaviour.Instantiate (edgePrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		line.GetComponent<EdgeObject> ().Init (vertexes.Get (x).VertexObject.GetComponent<VertexObject>(), vertexes.Get (y).VertexObject.GetComponent<VertexObject>(), cost);
	}

	public void AddEdge(string one, string two, int edgeCost){
		int x = vertexes.IndexOf (new Vertex (one));
		int y = vertexes.IndexOf (new Vertex (two));
		if(x == -1 || y == -1|| edgeCost == 0)
			return;
		AddEdge (x, y);

		GameObject line = MonoBehaviour.Instantiate (edgePrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		line.GetComponent<EdgeObject> ().Init (vertexes.Get (x).VertexObject.GetComponent<VertexObject>(), vertexes.Get (y).VertexObject.GetComponent<VertexObject>(), edgeCost);
	}

	public void RemoveEdge(int x, int y){
		if (brush != null) {
			brush.Reset ();
		}
		vertexes [x] [y] -= 1;
		vertexes [y] [x] -= 1;
	}

	public void RemoveEdge(string one, string two){
		int x = vertexes.IndexOf (new Vertex (one));
		int y = vertexes.IndexOf (new Vertex (two));
		if(x == -1 || y == -1 || vertexes [x] [y] == 0)
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
		output += info.PrintInfo ();
		return output;
	}

	public string CheckCycles(){
		string output = Print ();
		output += naiveCycles ();
		output += findCycleMultiplication ();
		return output;
	}

	private string naiveCycles(){
		int cyclesFound = 0;

		for (int i = 0; i < vertexes.Count; i++) {
			findCycleLength(i, new OList<Vertex>(), i, 3,  ref cyclesFound);
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

	private bool findCycleLength(int current, OList<Vertex> visited, int original, int lenght, ref int cyclesFound)
	{
		if (visited.Count == lenght-1) {
			if (vertexes [original] [current] == 1) {
				cyclesFound++;
				return true;
			} else {
				return false;
			}
		} else {
			visited.Add (vertexes[current]);
			for (int i = 0; i < vertexes.Count; i++) {
				if (vertexes [current] [i] == 1) {
					if (!visited.ToList().Contains (vertexes [i])) {
						OList<Vertex> x = new OList<Vertex> (visited.ToList ());
						findCycleLength (i, x, original, lenght, ref cyclesFound);
					}
				}
			}
		}

		return false;
	}
	#region Zadanie 2
	public string CheckCyclesOfLength(int x){
		string output = "";
		output += Print ();
		output += "\n\n FINDING CYCLE OF MIN(deg(matrix) + 1 \n\n ";
		output += "\nStarting from C" + x;
		for (int i = x; i <= vertexes.Count; i++) {
			OList<Vertex> cycle = naiveCycles (i);
			if (cycle != null) {
				string result = String.Join("->", cycle.ToList().Select(item => item.ToString()).ToArray());
				output += "\nFound cycle C"+ i +" :\n";
				output += result;
				brush.Paint (cycle);
				break;
			}
		}
		return output;
	}

	private OList<Vertex> naiveCycles(int x){
		for (int i = 0; i < vertexes.Count; i++) {
			OList<Vertex> q = findCycleLength(i, new OList<Vertex>(), i, x);
			if (q != null) {
				return q;
			}
		}
		return null;
	}

	private OList<Vertex> findCycleLength(int current, OList<Vertex> visited, int original, int lenght)
	{
		if (visited.Count == lenght-1) {
			if (vertexes [original] [current] == 1) {
				visited.Add (vertexes[current]);
				return visited;
			} else {
				return null;
			}
		} else {
			visited.Add (vertexes[current]);
			for (int i = 0; i < vertexes.Count; i++) {
				if (vertexes [current] [i] == 1) {
					if (!visited.ToList().Contains (vertexes [i])) {
						OList<Vertex> x = new OList<Vertex> (visited.ToList ());
						OList<Vertex> q = findCycleLength (i, x, original, lenght);
						if (q != null) {
							return q;
						}

					}
				}
			}
		}

		return null;
	}
	#endregion

	#region Jordan Algorithm

	public bool IsConsistent(){
		return consistency.IsConsistent (vertexes);
	}
		

	public string WriteVertexes(){
		string result = String.Join(" ", vertexes.ToList().Select(item => item.ToString()).ToArray());
		return result;
	}

	public bool HasCycle(){
		OList<Vertex> cycle = null;
		for (int cycleLength = 3; cycleLength <= vertexes.Count; cycleLength++) {
			for (int i = 0; i < vertexes.Count; i++) {
				OList<Vertex> q = findCycleLength (i, new OList<Vertex> (), i, cycleLength);
				if (q != null) {
					cycle = q;
					break;
				}
			}
		}

		if (cycle != null) {
			return true;
		}

		return false;
	}



	#endregion

	public OList<Vertex> FindUnseenBorderer(Vertex _currentVertex, OList<Vertex> _visited){
		return locationModule.FindUnseenBorderer (_currentVertex, _visited, this);
	}

	public void ResetEdges(){
		construct.ResetEdges (this);
	}

	public void InsertEdges(OList<EdgeStruct> _treeEdges){
		construct.InsertEdges (_treeEdges, this);
	}

	public void PaintConsistency(){
		consistency.PaintConsistency (vertexes, brush);
	}
}
