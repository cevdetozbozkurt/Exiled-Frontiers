using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class UIManager : MonoBehaviour
{
    private PlayerInputActions inputActions;

    [Header("UI Panels")]
    public GameObject inventoryUIPanel;

    [Header("Tab Pages")]
    public GameObject pageInventory;
    public GameObject pageCraft;
    public GameObject pageBuild;

    [Header("Inventory Drawing")]
    public GameObject itemSlotPrefab;
    public Transform inventoryGrid;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Inventory.Enable();
        inputActions.Inventory.ToggleInventory.performed += ToggleInventoryUI;
    }

    private void OnDisable()
    {
        inputActions.Inventory.ToggleInventory.performed -= ToggleInventoryUI;
        inputActions.Inventory.Disable();
    }

    private void Start()
    {
        inventoryUIPanel.SetActive(false);
        ShowInventoryPage();
    }

    private void ToggleInventoryUI(InputAction.CallbackContext context)
    {
        bool isActive = inventoryUIPanel.activeSelf;
        inventoryUIPanel.SetActive(!isActive);

        if(!isActive)
        {
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI()
    {

        for (int i = inventoryGrid.childCount - 1; i >= 0; i--)
        {
            Destroy(inventoryGrid.GetChild(i).gameObject);
        }

        foreach (var item in ResourceManager.Instance.inventory)
        {
            if(item.Value > 0) 
            { 
                GameObject newSlot = Instantiate(itemSlotPrefab, inventoryGrid, transform);

                TextMeshProUGUI textComp = newSlot.GetComponentInChildren<TextMeshProUGUI>();

                if(textComp != null )
                {
                    textComp.text = $"{item.Key}: {item.Value}";
                }
            }
        }
    }

    public void ShowInventoryPage()
    {
        pageInventory.SetActive(true);
        pageCraft.SetActive(false);
        pageBuild.SetActive(false);
        UpdateInventoryUI();
    }

    public void ShowCraftPage()
    {
        pageInventory.SetActive(false);
        pageCraft.SetActive(true);
        pageBuild.SetActive(false);
    }

    public void ShowBuildPage()
    {
        pageInventory.SetActive(false);
        pageCraft.SetActive(false);
        pageBuild.SetActive(true);
    }
}
