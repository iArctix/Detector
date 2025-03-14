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

        if (Physics.Raycast(ray, out hit, pickupRange, itemLayer))
        {
            if (hit.collider.CompareTag("Item"))
            {
                CollectibleItem item = hit.collider.GetComponent<CollectibleItem>();
                if (item != null && item.CanBeCollected())
                {
                    // Pass Quality & ActualPrice to inventory
                    InventoryManager.Instance.AddItem(item.itemData, item.Quality, item.ActualPrice);

                    Debug.Log($"Collected: {item.itemData.itemName} (Quality: {item.Quality}, Worth: ${item.ActualPrice})");

                    Destroy(hit.collider.gameObject); // Remove from world
                }
            }
        }
    }
}
