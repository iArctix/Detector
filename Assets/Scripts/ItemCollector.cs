using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public float pickupRange = 2f; // Max distance to pick up an item

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

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Item"))
            {
                Debug.Log("Collected: " + hit.collider.gameObject.name);
                Destroy(hit.collider.gameObject);
            }
        }
    }
}