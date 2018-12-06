using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputBehaviour : MonoBehaviour {
    public static InputBehaviour instance;

    public List<Button> sceneButtons;
    public List<InputField> sceneFields;

    private MatrixBehaviour matrix;
    private bool editMode = false;

    public GameObject CurrentSelectedGameObject { get; set; }

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
            editMode = !editMode;
            HandleObjects();
        }
    }

    void HandleObjects()
    {
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
        if(!editMode || matrix == null)
        {
            return;
        }

        HandleClick();
    }

    void HandleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CurrentSelectedGameObject != null)
            {
                //Start drawing line.
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

}
