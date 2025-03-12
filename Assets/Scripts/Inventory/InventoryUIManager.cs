using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryPanel; // UI Panel for inventory
    public Transform itemContainer; // RectTransform for item icons
    public GameObject itemIconPrefab; // Prefab for item icons

    public TMP_Text itemNameText, itemDescriptionText , itemPrice, itemQuality; // UI Text for item details
   

    public Transform itemSpawnPoint; // Where the item will appear on the table
    private GameObject currentSpawnedItem; // The currently spawned item

    private void Start()
    {
        inventoryPanel.SetActive(false); // Hide UI at start
        Debug.Log($"Inventory contains {InventoryManager.Instance.GetInventory().Count} items on scene load.");
    }

    public void ToggleInventory()
    {
        bool isActive = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isActive);

        if (isActive)
            PopulateInventory();
        else
            ClearSpawnedItem();
    }

    public void PopulateInventory()
    {
        // Clear existing icons
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        List<ItemData> inventory = InventoryManager.Instance.GetInventory();

        foreach (ItemData item in inventory)
        {
            GameObject icon = Instantiate(itemIconPrefab, itemContainer);
            icon.GetComponentInChildren<TMP_Text>().text = item.itemName;
            icon.GetComponent<Button>().onClick.AddListener(() => ShowItemDetails(item));
        }
    }

    void ShowItemDetails(ItemData item)
    {
        // Update UI details
        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.information;
        itemPrice.text = item.GetActualPrice().ToString();
        itemQuality.text = item.quality.ToString();


      

        // Remove previous item and spawn new one
        if (currentSpawnedItem != null)
        {
            Destroy(currentSpawnedItem);
        }

        if (item.itemModel != null) // Only spawn if item has a 3D model
        {
            currentSpawnedItem = Instantiate(item.itemModel, itemSpawnPoint.position, Quaternion.identity);
        }
    }

    public void ClearSpawnedItem()
    {
        if (currentSpawnedItem != null)
        {
            Destroy(currentSpawnedItem);
        }
    }
}