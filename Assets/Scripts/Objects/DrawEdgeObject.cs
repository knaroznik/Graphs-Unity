using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawEdgeObject : MonoBehaviour {
    

    protected LineRenderer render;
    public GameObject startLineOnbject { get; set; }
    public string StartVertexName { get; set;  }

    private void Start()
    {
        render = this.GetComponent<LineRenderer>();
    }

    void Update () {
        if (startLineOnbject != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f);
            render.SetPosition(0, startLineOnbject.transform.position);
            render.SetPosition(1, mousePosition);
        }
    }
}
