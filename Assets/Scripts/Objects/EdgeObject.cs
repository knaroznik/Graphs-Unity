using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdgeObject : MonoBehaviour {

	public VertexObject obj1;
	public VertexObject obj2;
	protected LineRenderer render;
    protected BoxCollider boxCollider;
    private int edgeCost;
    public int EdgeCost
    {
        get
        {
            return edgeCost;
        }

        set
        {
            edgeCost = value;
            costText.text = edgeCost.ToString();
            costText.gameObject.SetActive(true);

        }
    }

	protected bool isLoop = false;
    private Operator sign;
    public Operator Sign {
        get
        {
            return sign;
        }
        set
        {
            if(value == Operator.MINUS)
            {
                costText.color = Color.red;
            }
            sign = value;
        }
    }

	public Text costText;

	public virtual void Init(VertexObject one, VertexObject two, int _edgeCost){
		obj1 = one;
		obj2 = two;
		
		render = this.GetComponent<LineRenderer> ();
		if(obj1 == obj2){
			obj1.AddEdge (this);
			isLoop = true;
		}else{
			obj1.AddEdge (this);
			obj2.AddEdge (this);
		}

        boxCollider = GetComponent<BoxCollider>();

		edgeCost = _edgeCost;
		if (edgeCost != 1) {
			costText.text = edgeCost.ToString ();
			costText.gameObject.SetActive (true);
		}

        UpdateEdge();
	}

	public void UpdateEdge(){
		updatePosition ();
        updateCollider();
        upateCostPosition ();
	}

	protected virtual void updatePosition(){
		if (obj1 != null && obj2 != null) {
			if(!isLoop){
				render.SetPosition (0, obj1.transform.position);
				render.SetPosition (1, obj2.transform.position);
			}else{
				render.positionCount = 4;
				render.SetPosition (0, obj1.transform.position);
				render.SetPosition (1, obj1.transform.position + new Vector3(-1, 1, 0));
				render.SetPosition (2, obj1.transform.position + new Vector3(1, 1, 0));
				render.SetPosition (3, obj1.transform.position);
			}
		}
	}

	protected virtual void upateCostPosition(){
		if (isLoop)
			return;

		costText.gameObject.transform.SetPositionAndRotation ((obj1.transform.position + obj2.transform.position) / 2, Quaternion.identity);
	}

    protected void updateCollider()
    {
        float width = Vector3.Distance(obj1.transform.position, obj2.transform.position);
        boxCollider.size = new Vector3(1, 1, width * 0.9f);
        Vector3 midPoint = (obj1.transform.position + obj2.transform.position) / 2;
        boxCollider.transform.position = midPoint;
        boxCollider.center = Vector3.zero;
        boxCollider.transform.LookAt(obj1.transform.position);
    }

	public virtual bool IsSame(VertexObject one, VertexObject two){
		if (one == obj1 && two == obj2) {
			return true;
		}

		if (two == obj1 && one == obj2) {
			return true;
		}

		return false;
	}

	public virtual bool IsSame(EdgeObject edge){
		if (edge.obj1 == obj1 && edge.obj2 == obj2) {
			return true;
		}

		if (edge.obj2 == obj1 && edge.obj1 == obj2) {
			return true;
		}

		return false;
	}

	public void Destroy(){
		if(!isLoop){
		obj1.RemoveEdge (this);
		obj2.RemoveEdge (this);
		}else{
			obj1.RemoveEdge (this);
		}
		Destroy (this.gameObject);
	}


    private void OnMouseOver()
    {
        InputBehaviour.instance.CurrentSelectedGameObject = this.gameObject;
    }

    private void OnMouseExit()
    {
        InputBehaviour.instance.CurrentSelectedGameObject = null;
    }

    public override string ToString()
    {
        return "("+obj1.vertexData.VertexName + "," + obj2.vertexData.VertexName + ")";
    }

    //TODO : Jeśli pokolorowane to w dobrej kolejności.
    public MathEdgeStruct Copy()
    {
        return new MathEdgeStruct(obj1.vertexData.VertexName, obj1.transform.position, obj2.vertexData.VertexName, obj2.transform.position, edgeCost);
    }
}

public enum Operator { PLUS, MINUS}
