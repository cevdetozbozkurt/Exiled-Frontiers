using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class CraftingRecipe
{
    public string resultItemTag; 
    public string displayName;   
    public List<ResourceCost> costs; 
}

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

    [Header("Crafting Settings")]
    public List<CraftingRecipe> craftingRecipes; 
    public GameObject craftSlotPrefab; 
    public Transform craftGrid;

    [Header("Build Settings")]
    public GameObject buildSlotPrefab;
    public Transform buildGrid;

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
            UpdateCraftingUI();
            UpdateBuildUI();
        }
    }

    private void CloseUI()
    {
        inventoryUIPanel.SetActive(false);
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

    public void UpdateCraftingUI()
    {
        for (int i = craftGrid.childCount - 1; i >= 0; i--)
        {
            Destroy(craftGrid.GetChild(i).gameObject);
        }

        foreach (var recipe in craftingRecipes)
        {
            GameObject newSlot = Instantiate(craftSlotPrefab, craftGrid);
            TextMeshProUGUI textComp = newSlot.GetComponentInChildren<TextMeshProUGUI>();
            Button btnComp = newSlot.GetComponent<Button>();

            if (textComp != null)
            {
                string costText = "";
                foreach (var cost in recipe.costs)
                {
                    costText += $"{cost.amount} {cost.resourceTag}\n";
                }
                textComp.text = $"{recipe.displayName}\nCost:\n{costText}";
            }

            if (btnComp != null)
            {
                
                bool canCraft = ResourceManager.Instance.HasEnoughResources(recipe.costs);
                btnComp.interactable = canCraft;

                btnComp.onClick.AddListener(() => CraftItem(recipe));
            }
        }
    }

    public void UpdateBuildUI()
    {
        for (int i = buildGrid.childCount - 1; i >= 0; i--)
        {
            Destroy(buildGrid.GetChild(i).gameObject);
        }

        if (BuildingManager.Instance == null) return;

        foreach (var buildItem in BuildingManager.Instance.buildableObjects)
        {
            GameObject newSlot = Instantiate(buildSlotPrefab, buildGrid);
            TextMeshProUGUI textComp = newSlot.GetComponentInChildren<TextMeshProUGUI>();
            Button btnComp = newSlot.GetComponent<Button>();

            int ownedAmount = ResourceManager.Instance.GetResourceAmount(buildItem.itemTag);

            if (textComp != null)
            {
                textComp.text = $"Build: {buildItem.itemTag}\nOwned: {ownedAmount}";
            }

            if (btnComp != null)
            {
                btnComp.interactable = ownedAmount > 0;

                btnComp.onClick.AddListener(() =>
                {
                    BuildingManager.Instance.StartPlacement(buildItem.itemTag);
                    CloseUI();
                });
            }
        }
    }

    private void CraftItem(CraftingRecipe recipe)
    {
        if (ResourceManager.Instance.HasEnoughResources(recipe.costs))
        {
            ResourceManager.Instance.SpendResources(recipe.costs);
            ResourceManager.Instance.AddResource(recipe.resultItemTag, 1);

            UpdateInventoryUI();
            UpdateCraftingUI();
            UpdateBuildUI();
            Debug.Log($"Successfully crafted: {recipe.displayName}");
        }
    }

    public void ShowInventoryPage()
    {
        pageInventory.SetActive(true);
        pageCraft.SetActive(false);
        pageBuild.SetActive(true);
        pageBuild.SetActive(false);
        UpdateInventoryUI();
    }

    public void ShowCraftPage()
    {
        pageInventory.SetActive(false);
        pageCraft.SetActive(true);
        pageBuild.SetActive(false);
        UpdateCraftingUI();
    }

    public void ShowBuildPage()
    {
        pageInventory.SetActive(false);
        pageCraft.SetActive(false);
        pageBuild.SetActive(true);
        UpdateBuildUI();
    }

}
