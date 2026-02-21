using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct ResourceCost
{
    public string resourceTag;
    public int amount;
}

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public Dictionary<string, int> inventory = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    public void AddResource(string resourceTag, int amount)
    {
        if (inventory.ContainsKey(resourceTag))
        {
            inventory[resourceTag] += amount;
        }
        else
        {
            inventory.Add(resourceTag, amount);
        }

        Debug.Log($"Envantere Eklendi: {resourceTag}. Toplam miktar: {inventory[resourceTag]}");
    }

    public int GetResourceAmount(string resourceTag)
    {
        if(inventory.ContainsKey(resourceTag)) return inventory[resourceTag];
        return 0;
    }

    public bool HasResource(string resourceTag, int requiredAmount)
    {
        return inventory.ContainsKey(resourceTag) && inventory[resourceTag] >= requiredAmount;
    }

    public bool HasEnoughResources(List<ResourceCost> costs)
    {
        foreach (var cost in costs)
        {
            if (!HasResource(cost.resourceTag, cost.amount)) return false;
        }
        return true;
    }

    public void SpendResources(List<ResourceCost> costs)
    {
        if (!HasEnoughResources(costs)) return;

        foreach (var cost in costs)
        {
            inventory[cost.resourceTag] -= cost.amount;
        }
    }
}
