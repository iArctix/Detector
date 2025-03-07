using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class RadarSystem : MonoBehaviour
{
    public Transform metalDetector;  // Reference to the Metal Detector's position
    public float detectionRadius = 5f; // Detection range
    public Transform sweepLine;  // UI Sweep Line
    public GameObject blipPrefab; // Blip prefab
    public Transform blipContainer; // Parent for blips in UI
    public TextMeshProUGUI distanceText; // UI for distance
    public TextMeshProUGUI depthText; // UI for depth

    private float sweepSpeed = 100f; // Sweep speed
    private GameObject activeBlip; // Current active blip
    private GameObject nearestItem; // Closest detected item

    void Start()
    {
        activeBlip = Instantiate(blipPrefab, blipContainer);
        activeBlip.SetActive(false); // Start hidden
    }

    void Update()
    {
        RotateSweepLine();
        TrackNearestItem();
    }

    void RotateSweepLine()
    {
        sweepLine.Rotate(0, 0, -sweepSpeed * Time.deltaTime);
    }

    void TrackNearestItem()
    {
        nearestItem = null;
        float nearestDistance = detectionRadius;
        float detectedDepth = 0;

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
        {
            Vector3 itemPosition = item.transform.position;
            Vector3 detectorPosition = new Vector3(metalDetector.position.x, 0, metalDetector.position.z);
            Vector3 itemFlatPosition = new Vector3(itemPosition.x, 0, itemPosition.z);

            // **Flat distance (ignoring depth)**
            float flatDistance = Vector3.Distance(detectorPosition, itemFlatPosition);
            float depth = itemPosition.y - metalDetector.position.y; // Keep depth for display

            if (flatDistance <= detectionRadius && flatDistance < nearestDistance)
            {
                nearestDistance = flatDistance;
                nearestItem = item;
                detectedDepth = depth;
            }
        }

        if (nearestItem != null)
        {
            UpdateBlip(nearestItem.transform.position, nearestDistance);
            UpdateUI(nearestDistance, detectedDepth);
        }
        else
        {
            activeBlip.SetActive(false);
        }
    }

    void UpdateBlip(Vector3 itemPosition, float distance)
    {
        activeBlip.SetActive(true);

        // Get relative position of item **from detector's head** (not player)
        Vector3 relativePosition = itemPosition - metalDetector.position;
        float angle = Mathf.Atan2(relativePosition.z, relativePosition.x) * Mathf.Rad2Deg;

        // Normalize distance (closer = closer to radar center)
        float normalizedDistance = Mathf.Clamp01(distance / detectionRadius);

        // **Fix Blip Positioning (Invert Y)**
        Vector3 blipPosition = new Vector3(
            Mathf.Cos(angle),
            -Mathf.Sin(angle),
            0
        ) * (normalizedDistance * 200);

        activeBlip.GetComponent<RectTransform>().anchoredPosition = blipPosition;
    }

    void UpdateUI(float distance, float depth)
    {
        distanceText.text = "Distance: " + distance.ToString("F1") + "m";
        depthText.text = "Depth: " + depth.ToString("F1") + "m";
    }
}
