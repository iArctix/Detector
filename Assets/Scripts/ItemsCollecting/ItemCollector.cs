using UnityEngine;
using System.Collections.Generic;

public class ItemCollector : MonoBehaviour
{
    public float pickupRange = 2f; // Max distance to pick up an item
    public LayerMask itemLayer; // Set this to the "ItemLayer" in the Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickupItem();
        }
    }

    void TryPickupItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange, itemLayer)) // Only hits objects on ItemLayer
        {
            if (hit.collider.CompareTag("Item")) // Ensure it's tagged correctly
            {
                CollectibleItem item = hit.collider.GetComponent<CollectibleItem>();
                if (item != null && item.CanBeCollected())
                {
                    // Use InventoryManager to add the item to the inventory
                    InventoryManager.Instance.AddItem(item.itemData);
                    Debug.Log($"Collected: {item.itemData.itemName} (Quality: {item.Quality}, Worth: ${item.ActualPrice})");

                    Destroy(hit.collider.gameObject); // Remove from world
                }
                else
                {
                    Debug.Log("Item cannot be collected.");
                }
            }
        }
        else
        {
            Debug.Log("No item detected.");
        }
    }
}
