using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MetalDetector : MonoBehaviour
{
    public float detectionRange = 5f; // How far the detector can scan
    public float maxDepth = 1f; // Maximum depth the detector can detect
    public Transform detectorHead; // The actual metal detector's scanning position
    public Transform playerCamera; // Player's camera for direction calculations
    public LayerMask itemLayer; // Layer for detecting items

    public RectTransform radarArrowUI; // UI arrow pointing to the nearest item
    public TextMeshProUGUI distanceText; // UI text to display item distance
    public TextMeshProUGUI depthText; // UI text to display item depth

    private GameObject nearestItem;
    private float nearestItemDistance;
    private float nearestItemDepth;

    void Update()
    {
        ScanForItems();
        UpdateRadarArrow();
        UpdateUI();
    }

    void ScanForItems()
    {
        Collider[] items = Physics.OverlapSphere(detectorHead.position, detectionRange, itemLayer);
        float closestDistance = Mathf.Infinity;
        nearestItem = null;

        foreach (Collider item in items)
        {
            Vector3 flatDetectorPos = new Vector3(detectorHead.position.x, 0, detectorHead.position.z);
            Vector3 flatItemPos = new Vector3(item.transform.position.x, 0, item.transform.position.z);

            float distance = Vector3.Distance(flatDetectorPos, flatItemPos); // 2D Distance (X, Z)
            float itemDepth = Mathf.Abs(detectorHead.position.y - item.transform.position.y); // Depth (Y only)

            if (itemDepth <= maxDepth && distance < closestDistance) // Check depth & find closest
            {
                closestDistance = distance;
                nearestItem = item.gameObject;
                nearestItemDistance = distance;
                nearestItemDepth = itemDepth;
            }
        }
    }

    void UpdateRadarArrow()
    {
        if (nearestItem != null)
        {
            Vector3 toItem = new Vector3(nearestItem.transform.position.x, detectorHead.position.y, nearestItem.transform.position.z) - detectorHead.position;
            toItem.Normalize();

            float angle = Vector3.SignedAngle(playerCamera.forward, toItem, Vector3.up);

            // Smoothly rotate the arrow towards the item
            radarArrowUI.rotation = Quaternion.Lerp(radarArrowUI.rotation, Quaternion.Euler(0, 0, -angle), Time.deltaTime * 10f);
            radarArrowUI.gameObject.SetActive(true);
        }
        else
        {
            radarArrowUI.gameObject.SetActive(false); // Hide arrow if no item found
        }
    }

    void UpdateUI()
    {
        if (nearestItem != null)
        {
            distanceText.text = "Distance: " + nearestItemDistance.ToString("F1") + "m";
            depthText.text = "Depth: " + nearestItemDepth.ToString("F1") + "m";
        }
        else
        {
            distanceText.text = "Distance: ---";
            depthText.text = "Depth: ---";
        }
    }
}
