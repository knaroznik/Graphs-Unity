using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyCreator : MonoBehaviour {

    private Graph galaxy;

	// Use this for initialization
	void Start () {
        galaxy = GetComponent<MatrixBehaviour>().matrix;
        Create(4);
	}
	
	public void Create(int _galaxySize)
    {
        for(int i=0; i<_galaxySize; i++)
        {
            //Some space for each system.
            galaxy.AddVertex(i.ToString());
        }

        while (!galaxy.IsConsistent())
        {
            int x = Random.Range(0, galaxy.Size);
            int y = Random.Range(0, galaxy.Size);
            Vertex one = galaxy.GetVertex(x);
            Vertex two = galaxy.GetVertex(y);
            

            //Check if graph will still be planar.
            if(x != y && galaxy.GetEdge(one, two) == null)
            {
                galaxy.AddEdge(one.VertexName, two.VertexName, 1);
            }

        }
    }
}
