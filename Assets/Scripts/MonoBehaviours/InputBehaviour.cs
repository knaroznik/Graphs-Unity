using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputBehaviour : MonoBehaviour {

    public static InputBehaviour instance;

    public List<Button> sceneButtons;
    public List<InputField> sceneFields;

    public GameObject linePrefab;
    public Text inputModeName;

    private MatrixBehaviour matrix;
    private InputMode inputMode = InputMode.IDLE;

    public GameObject CurrentSelectedGameObject { get; set; }
    private DrawEdgeObject CurrentDrawingLine;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        matrix = GetComponent<MatrixBehaviour>();
    }

    // Update is called once per frame
    void Update () {
        HandleSwitch();
        HandleInput();
	}

    void HandleSwitch()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(inputMode == InputMode.IDLE)
            {
                inputMode = InputMode.EDIT;
            }else if (inputMode == InputMode.EDIT)
            {
                inputMode = InputMode.DELETE;
            }else if (inputMode == InputMode.DELETE)
            {
                inputMode = InputMode.IDLE;
            }
            inputModeName.text = inputMode.ToString();
            HandleObjects();
        }
    }

    void HandleObjects()
    {
        bool editMode = true;
        if(inputMode == InputMode.IDLE)
        {
            editMode = false;
        }

        for(int i=0; i<sceneButtons.Count; i++)
        {
            sceneButtons[i].interactable = !editMode;
        }

        for (int i = 0; i < sceneFields.Count; i++)
        {
            sceneFields[i].interactable = !editMode;
        }
    }

    void HandleInput()
    {
        if(matrix == null)
        {
            return;
        }

        if (inputMode == InputMode.IDLE){
            HandleDrag();
        }
        else if(inputMode == InputMode.EDIT){
            HandleClick();
        }else if(inputMode == InputMode.DELETE){
            HandleDelete();
        }

        
    }

    void HandleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CurrentSelectedGameObject != null)
            {
                if (CurrentDrawingLine != null)
                {
                    EndLine(CurrentSelectedGameObject);
                }
                else
                {
                    DrawLine(CurrentSelectedGameObject);
                }
            }
            else
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f);

                matrix.matrix.AddNewVertex(mousePosition);
                matrix.Print();
            }
        }
    }

    void HandleDrag()
    {

    }

    void HandleDelete()
    {

    }

    void DrawLine(GameObject startLine)
    {
        GameObject line = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        line.GetComponent<DrawEdgeObject>().startLineOnbject = startLine;
        line.GetComponent<DrawEdgeObject>().StartVertexName = startLine.GetComponent<VertexObject>().vertexData.VertexName;
        CurrentDrawingLine = line.GetComponent<DrawEdgeObject>();
    }

    void EndLine(GameObject endLine)
    {
        string start = CurrentDrawingLine.StartVertexName;
        string end = endLine.GetComponent<VertexObject>().vertexData.VertexName;
        //TODO : Koszt krawędzi

        if(start != end)
        {
            matrix.matrix.AddEdge(start, end, 1);
        }

        Destroy(CurrentDrawingLine.gameObject);
        CurrentDrawingLine = null;
    }

}

public enum InputMode { IDLE, EDIT, DELETE}