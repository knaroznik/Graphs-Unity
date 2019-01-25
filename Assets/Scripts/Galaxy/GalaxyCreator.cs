using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyCreator : MonoBehaviour {

    private Graph galaxy;
    private float freeSpaceperGalaxy = 5;

	// Use this for initialization
	void Start () {
        galaxy = GetComponent<MatrixBehaviour>().matrix;
        Create(20);
	}
	
	public void Create(int _galaxySize)
    {
        for(int i=0; i<_galaxySize; i++)
        {
            //Some space for each system.
            galaxy.construct.AddVertex(i.ToString(), GetFreeSpace(galaxy.vertexes));
        }

        while (!galaxy.IsConsistent())
        {
            int x = Random.Range(0, galaxy.Size);
            int y = Random.Range(0, galaxy.Size);
            Vertex one = galaxy.GetVertex(x);
            Vertex two = galaxy.GetVertex(y);
            

            //Check if graph will still be planar.
            if(x != y && galaxy.locationModule.GetEdge(one, two) == null)
            {
                galaxy.construct.AddEdge(one.VertexName, two.VertexName, 1);
            }

        }
    }

    private Vector3 GetFreeSpace(OList<Vertex> _vertexes)
    {
        Vector3? freeSpace = null;
        do
        {
            float x = Random.Range(-50f, 50f);
            float y = Random.Range(-30f, 30f);
            int correctPlaced = 0;
            for(int i=0; i<_vertexes.Count; i++)
            {
                if(Vector3.Distance(new Vector3(x,y,0f), _vertexes[i].VertexObject.transform.position) > freeSpaceperGalaxy)
                {
                    correctPlaced++;
                }
            }

            if (correctPlaced == _vertexes.Count)
            {
                freeSpace = new Vector3(x, y, 0f);
            }
        } while (freeSpace == null);
        return freeSpace.Value;
    }
}
