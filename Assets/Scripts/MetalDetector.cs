using UnityEngine;
using UnityEngine.UI;

public class MetalDetector : MonoBehaviour
{
    public float detectionRange = 5f; // How far the detector can scan
    public float detectionAngle = 30f; // Angle of the detection cone
    public float maxDepth = 1f; // Maximum depth the detector can detect

    public Transform playerCamera; // Reference to the player's camera
    public LayerMask itemLayer; // LayerMask for detecting items
    public RectTransform radarArrowUI; // UI arrow to point toward the nearest item

    private GameObject nearestItem;

    void Update()
    {
        ScanForItems();
        UpdateRadarArrow();
    }

    void ScanForItems()
    {
        Collider[] items = Physics.OverlapSphere(transform.position, detectionRange, itemLayer);
        float closestDistance = Mathf.Infinity;
        nearestItem = null;

        foreach (Collider item in items)
        {
            Vector3 directionToItem = (item.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(playerCamera.forward, directionToItem);
            float itemDepth = item.transform.position.y - transform.position.y; // Check item depth

            if (angle < detectionAngle && itemDepth <= maxDepth) // Is item in front & within depth?
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestItem = item.gameObject;
                }
            }
        }
    }

    void UpdateRadarArrow()
    {
        if (nearestItem != null)
        {
            Vector3 toItem = nearestItem.transform.position - transform.position;
            float angle = Vector3.SignedAngle(playerCamera.forward, toItem, Vector3.up);

            // Convert world angle to UI rotation
            radarArrowUI.rotation = Quaternion.Euler(0, 0, -angle);
            radarArrowUI.gameObject.SetActive(true);
        }
        else
        {
            radarArrowUI.gameObject.SetActive(false); // Hide arrow if no item found
        }
    }
}
