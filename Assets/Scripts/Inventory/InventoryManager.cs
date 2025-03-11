using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private List<ItemData> inventory = new List<ItemData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps inventory across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Add an item to the inventory
    public void AddItem(ItemData item)
    {
        inventory.Add(item);
        Debug.Log($"Added {item.itemName} to inventory!");
    }

    // Get all items in inventory
    public List<ItemData> GetInventory()
    {
        return inventory;
    }
}