using UnityEngine;
using UnityEngine.InputSystem;
public class UIManager : MonoBehaviour
{
    private PlayerInputActions inputActions;

    [Header("UI Panels")]
    public GameObject inventoryUIPanel;

    [Header("Tab Pages")]
    public GameObject pageInventory;
    public GameObject pageCraft;
    public GameObject pageBuild;

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
    }

    public void ShowInventoryPage()
    {
        pageInventory.SetActive(true);
        pageCraft.SetActive(false);
        pageBuild.SetActive(false);
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
