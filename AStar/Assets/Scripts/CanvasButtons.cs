using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasButtons : MonoBehaviour
{
    AddStructure addStructure;

    [SerializeField] GridSystem gridSystem;
    [SerializeField] CameraController cameraController;

    [SerializeField] GameObject demoEnvironment;
    [SerializeField] GameObject structurePrefab;
    [SerializeField] GameObject playersEnvironment;

    private bool isPlacing;
    public bool IsPlacing
    {
        get { return isPlacing; }
        set { isPlacing = value; }
    }


    private void Start()
    {
        addStructure = new AddStructure(structurePrefab, playersEnvironment.transform, gridSystem, this);
    }


    public void ClearAllStructures()
    {
        StartCoroutine(ClearAllStructuresCoroutine());
    }
    IEnumerator ClearAllStructuresCoroutine()
    {
        demoEnvironment.SetActive(false);

        foreach (Transform child in playersEnvironment.transform)
        {
            Destroy(child.gameObject);
        }

        yield return null;
        // Destroy method does not destroy objects immediately.
        // It prevents the ConfigureGrid method from functioning properly for that Wait for one frame

        gridSystem.ConfigureGrid();
    }

    public void ShowDemoStructures()
    {
        demoEnvironment.SetActive(true);
        gridSystem.ConfigureGrid();
    }

    public void AddStructure()
    {
        StartCoroutine(addStructure.PlaceStructure());
    }

    public void FreeView()
    {
        cameraController.SetFreeView();
    }

    public void UpperView()
    {
        cameraController.SetUpperView();
    }

    public void Quit()
    {
        Application.Quit();
    }
}






public class AddStructure
{

    GameObject structurePrefab;
    Transform playersEnvironmentTransform;
    GridSystem gridSystem;
    CanvasButtons canvasButtons;

    Camera mainCamera;
    float groundMinX, groundMaxX, groundMinZ, groundMaxZ;
    float structureRotationSpeed = 500f;

    string groundTag = "Ground";
    int rotationButtonIndex = 1;
    int placingButtonIndex = 0;

    public AddStructure(GameObject structurePrefab, Transform playersEnvironmentTransform, GridSystem gridSystem, CanvasButtons canvasButtons)
    {
        this.structurePrefab = structurePrefab;
        this.playersEnvironmentTransform = playersEnvironmentTransform;
        this.gridSystem = gridSystem;
        this.canvasButtons = canvasButtons;

        mainCamera = Camera.main;
        GetGroundBoundaries(out groundMinX, out groundMaxX, out groundMinZ, out groundMaxZ);
    }

    private void GetGroundBoundaries(out float minX, out float maxX, out float minZ, out float maxZ)
    {
        Renderer groundRenderer = GameObject.FindGameObjectWithTag(groundTag).GetComponent<Renderer>();
        minX = groundRenderer.transform.position.x - groundRenderer.bounds.size.x / 2;
        maxX = groundRenderer.transform.position.x + groundRenderer.bounds.size.x / 2;
        minZ = groundRenderer.transform.position.z - groundRenderer.bounds.size.z / 2;
        maxZ = groundRenderer.transform.position.z + groundRenderer.bounds.size.z / 2;
    }

    public IEnumerator PlaceStructure()
    {
        GameObject structure = Object.Instantiate(structurePrefab, playersEnvironmentTransform);
        canvasButtons.IsPlacing = true;

        while (true)
        {
            MoveStructureToMousePosition(structure, groundMinX, groundMaxX, groundMinZ, groundMaxZ);

            if (Input.GetMouseButton(rotationButtonIndex))
            {
                RotateStructure(structure);
            }

            if (Input.GetMouseButton(placingButtonIndex))
            {
                break;
            }

            yield return null;
        }

        canvasButtons.IsPlacing = false;
        gridSystem.ConfigureGrid();
    }

    private void MoveStructureToMousePosition(GameObject structure, float minX, float maxX, float minZ, float maxZ)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.WorldToScreenPoint(structure.transform.position).z;
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
        structure.transform.position = newPosition;
    }
    private void RotateStructure(GameObject structure)
    {
        float rotationSpeed = structureRotationSpeed * Time.deltaTime;
        structure.transform.rotation *= Quaternion.Euler(0, rotationSpeed, 0);

    }
}