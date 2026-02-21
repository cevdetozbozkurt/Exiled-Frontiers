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
            currentGhost.transform.position = hit.point;
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

    private void PlaceBuilding()
    {
        ResourceManager.Instance.SpendResource(currentBuildingData.itemTag, 1);

        Instantiate(currentBuildingData.realPrefab, currentGhost.transform.position, currentGhost.transform.rotation);

        Debug.Log($"{currentBuildingData.itemTag} placed successfully! Waiting for a builder...");

        if (ResourceManager.Instance.GetResourceAmount(currentBuildingData.itemTag) <= 0)
        {
            CancelPlacement();
        }
        else
        {
            UIManager uiManager = FindFirstObjectByType<UIManager>();

            if (uiManager != null)
            {
                uiManager.UpdateInventoryUI();
            }
        }
    }

    public void CancelPlacement()
    {
        isPlacing = false;
        currentBuildingData = null;
        if (currentGhost != null)
        {
            Destroy(currentGhost);
        }
    }
}
