using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[System.Serializable]
public class BuildableObject
{
    public string itemTag;        
    public GameObject ghostPrefab; 
    public GameObject realPrefab; 
}

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    [Header("Placement Settings")]
    public LayerMask groundLayer;

    public float rotationStep = 45f;

    public float gridSize = 2f;

    [Header("Buildable Objects Database")]
    public List<BuildableObject> buildableObjects;

    private GameObject currentGhost;
    private BuildableObject currentBuildingData;
    private bool isPlacing = false;

    private float currentYRotation = 0f;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    public void StartPlacement(string itemTag)
    {
        if (ResourceManager.Instance.GetResourceAmount(itemTag) <= 0)
        {
            Debug.LogWarning($"Not enough {itemTag} in inventory to build!");
            return;
        }

        currentBuildingData = buildableObjects.Find(b => b.itemTag == itemTag);

        if (currentBuildingData == null)
        {
            Debug.LogError($"No configuration found for {itemTag} in BuildingManager list!");
            return;
        }

        isPlacing = true;
        currentYRotation = 0f;

        if (currentGhost != null) Destroy(currentGhost);

        currentGhost = Instantiate(currentBuildingData.ghostPrefab);
    }

    private void Update()
    {
        if (!isPlacing || currentGhost == null) return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentYRotation += rotationStep;
        }
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            currentYRotation -= rotationStep;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
        {
            float snappedX = Mathf.Round(hit.point.x / gridSize) * gridSize;
            float snappedZ = Mathf.Round(hit.point.z / gridSize) * gridSize;

            float yOffset = GetPivotToBottomOffset(currentGhost);
            Vector3 snappedPosition = new Vector3(snappedX, hit.point.y + yOffset, snappedZ);

            currentGhost.transform.position = snappedPosition;
            currentGhost.transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            PlaceBuilding();
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            CancelPlacement();
        }
    }

    private float GetPivotToBottomOffset(GameObject obj)
    {
        Renderer renderer = obj.GetComponentInChildren<Renderer>();
        if (renderer == null) return 0f;

        return obj.transform.position.y - renderer.bounds.min.y;
    }

    private void PlaceBuilding()
    {
        ResourceManager.Instance.SpendResource(currentBuildingData.itemTag, 1);

        GameObject siteObj = Instantiate(currentBuildingData.ghostPrefab, currentGhost.transform.position, currentGhost.transform.rotation);

        ConstructionSite site = siteObj.AddComponent<ConstructionSite>();
        site.Initialize(currentBuildingData.realPrefab, 5.0f);

        AssignBuilderToSite(siteObj.transform);
        CancelPlacement();
    }

    private void AssignBuilderToSite(Transform siteTransform)
    {
        WorkerController[] allWorkers = FindObjectsByType<WorkerController>(FindObjectsSortMode.None);
        foreach (var worker in allWorkers)
        {
            if (worker.CompareTag("Builder"))
            {
                worker.SetBuildTarget(siteTransform);
                break;
            }
        }
    }

    public void CancelPlacement()
    {
        isPlacing = false;
        if (currentGhost != null) Destroy(currentGhost);
    }
}
