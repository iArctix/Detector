using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    // Inventory stores full item data with Quality & Price
    private List<InventoryItem> inventory = new List<InventoryItem>();

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

    // Class to store item with Quality & Actual Price
    public class InventoryItem
    {
        public ItemData itemData;
        public int Quality;
        public int ActualPrice;

        public InventoryItem(ItemData data, int quality, int price)
        {
            itemData = data;
            Quality = quality;
            ActualPrice = price;
        }
    }

    // Add an item to the inventory with quality and price
    public void AddItem(ItemData itemData, int quality, int actualPrice)
    {
        InventoryItem newItem = new InventoryItem(itemData, quality, actualPrice);
        inventory.Add(newItem);

        Debug.Log($"Added {itemData.itemName} to inventory! Quality: {quality}, Worth: ${actualPrice}");
    }

    // Get all items in inventory
    public List<InventoryItem> GetInventory()
    {
        return inventory;
    }
}
