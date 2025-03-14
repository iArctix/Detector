using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryPanel; // UI Panel for inventory
    public Transform itemContainer; // RectTransform for item icons
    public GameObject itemIconPrefab; // Prefab for item icons

    public TMP_Text itemNameText, itemDescriptionText, itemPrice, itemQuality; // UI Text for item details

    public Transform itemSpawnPoint; // Where the item appears in inventory
    public Transform inspectPoint; // Where the item moves for inspection

    public Camera storageCamera; // Inventory camera
    public Camera inspectCamera; // Inspection camera

    public Button inspectButton; // Button to inspect item
    public GameObject inspectModeUI; // UI for inspect mode
    private bool isInspecting = false;

    private GameObject currentSpawnedItem; // The currently spawned item
    private Vector3 originalPosition; // Stores original position before inspect
    private Quaternion originalRotation; // Stores original rotation before inspect

    private void Start()
    {
        inventoryPanel.SetActive(false); // Hide UI at start
        inspectModeUI.SetActive(false); // Hide inspect UI at start
        inspectButton.gameObject.SetActive(false); // Hide inspect button initially
        inspectCamera.gameObject.SetActive(false); // Ensure inspect camera is off

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

        List<InventoryManager.InventoryItem> inventory = InventoryManager.Instance.GetInventory();

        foreach (InventoryManager.InventoryItem inventoryItem in inventory)
        {
            GameObject icon = Instantiate(itemIconPrefab, itemContainer);
            icon.GetComponent<Image>().sprite = inventoryItem.itemData.itemIcon; // Set icon image
            icon.GetComponent<Button>().onClick.AddListener(() => ShowItemDetails(inventoryItem));
        }
    }

    void ShowItemDetails(InventoryManager.InventoryItem inventoryItem)
    {
        ItemData item = inventoryItem.itemData; // Get base item details

        // Update UI details
        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.information;
        itemPrice.text = $"${inventoryItem.ActualPrice}"; // Show item's calculated price
        itemQuality.text = $"Quality: {inventoryItem.Quality}"; // Show item's unique quality

        // Remove previous item and spawn new one
        if (currentSpawnedItem != null)
        {
            Destroy(currentSpawnedItem);
        }

        if (item.itemModel != null) // Only spawn if item has a 3D model
        {
            currentSpawnedItem = Instantiate(item.itemModel, itemSpawnPoint.position, Quaternion.identity);
            originalPosition = currentSpawnedItem.transform.position;
            originalRotation = currentSpawnedItem.transform.rotation;
            inspectButton.gameObject.SetActive(true); // Show Inspect button
        }
    }

    public void EnterInspectMode()
    {
        if (currentSpawnedItem == null) return;

        isInspecting = true;

        // Switch cameras
        storageCamera.gameObject.SetActive(false);
        inspectCamera.gameObject.SetActive(true);

        // Move object to inspect position
        currentSpawnedItem.transform.position = inspectPoint.position;
        currentSpawnedItem.transform.rotation = Quaternion.identity;

        // Hide inventory UI, show inspect UI
        inventoryPanel.SetActive(false);
        inspectModeUI.SetActive(true);
    }

    public void ExitInspectMode()
    {
        if (currentSpawnedItem == null) return;

        isInspecting = false;

        // Switch cameras back
        inspectCamera.gameObject.SetActive(false);
        storageCamera.gameObject.SetActive(true);

        // Move object back to original position
        currentSpawnedItem.transform.position = originalPosition;
        currentSpawnedItem.transform.rotation = originalRotation;

        // Show inventory UI, hide inspect UI
        inventoryPanel.SetActive(true);
        inspectModeUI.SetActive(false);
    }

    public void ClearSpawnedItem()
    {
        if (currentSpawnedItem != null)
        {
            Destroy(currentSpawnedItem);
        }

        inspectButton.gameObject.SetActive(false); // Hide Inspect button
    }

    private void Update()
    {
        if (isInspecting && currentSpawnedItem != null)
        {
            RotateItem();
        }
    }

    private void RotateItem()
    {
        if (Input.GetMouseButton(0)) // Left mouse button held down
        {
            float rotationSpeed = 5f;
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed;

            currentSpawnedItem.transform.Rotate(Vector3.up, -rotX, Space.World);
            currentSpawnedItem.transform.Rotate(Vector3.right, rotY, Space.World);
        }
    }
}
