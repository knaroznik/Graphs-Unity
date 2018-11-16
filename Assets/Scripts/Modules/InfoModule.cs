using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoModule {

	Graph dataMatrix;
	OList<Vertex> vertexes;

	public InfoModule(Graph _matrix){
		dataMatrix = _matrix;
	}

	public string PrintInfo(){
		vertexes = dataMatrix.vertexes;
		string output = "";
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

	public int LowestValue(){
		int min = 999;
		for(int i=0; i<vertexes.Count; i++){
			int x = vertexes[i].Value();
			if(x < min)
				min = x;
		}
		return min;
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
}
