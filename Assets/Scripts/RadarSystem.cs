using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class RadarSystem : MonoBehaviour
{
    public Transform metalDetector; // The metal detector/player object
    public float detectionRadius = 5f; // How far the radar can detect items
    public Transform sweepLine; // The rotating UI sweep line
    public GameObject blipPrefab; // Blip prefab
    public Transform blipContainer; // Parent for blips in UI
    public TextMeshProUGUI distanceText; // UI for distance
    public TextMeshProUGUI depthText; // UI for depth
    public float radarRangeUI = 220f; // UI radius scaling for the radar display
    public float fadeSpeed = 2f; // Speed of fading effect

    private float sweepSpeed = 100f; // Sweep rotation speed
    private Dictionary<GameObject, GameObject> activeBlips = new Dictionary<GameObject, GameObject>(); // Map items to their blips
    private float sweepAngleOffset = 90f; // ?? Corrects the 90-degree error

    void Update()
    {
        RotateSweepLine();
        TrackItems();
    }

    void RotateSweepLine()
    {
        sweepLine.Rotate(0, 0, -sweepSpeed * Time.deltaTime);
    }

    void TrackItems()
    {
        HashSet<GameObject> detectedItems = new HashSet<GameObject>();

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
        {
            Vector3 itemPosition = item.transform.position;
            Vector3 detectorPosition = new Vector3(metalDetector.position.x, 0, metalDetector.position.z);
            Vector3 itemFlatPosition = new Vector3(itemPosition.x, 0, itemPosition.z);

            float flatDistance = Vector3.Distance(detectorPosition, itemFlatPosition);

            if (flatDistance <= detectionRadius)
            {
                detectedItems.Add(item);

                if (!activeBlips.ContainsKey(item))
                {
                    GameObject newBlip = Instantiate(blipPrefab, blipContainer);
                    activeBlips[item] = newBlip;

                    // ?? Set new blips to be fully transparent at start
                    newBlip.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                }

                UpdateBlip(item, activeBlips[item], flatDistance);
            }
        }

        RemoveOutOfRangeBlips(detectedItems);
    }

    void UpdateBlip(GameObject item, GameObject blip, float distance)
    {
        RectTransform blipTransform = blip.GetComponent<RectTransform>();
        Image blipImage = blip.GetComponent<Image>();

        // Get relative position of item in local space
        Vector3 relativePosition = metalDetector.InverseTransformPoint(item.transform.position);

        // Scale position so that max range aligns with radar UI size
        float scaledX = (relativePosition.x / detectionRadius) * radarRangeUI;
        float scaledY = (relativePosition.z / detectionRadius) * radarRangeUI;

        blipTransform.anchoredPosition = new Vector2(scaledX, scaledY);

        // Calculate angle difference between blip and sweep line
        float blipAngle = Mathf.Atan2(scaledY, scaledX) * Mathf.Rad2Deg;
        float sweepAngle = sweepLine.eulerAngles.z + sweepAngleOffset; // ?? Offset the sweep angle by 90 degrees

        // Convert both angles to a 0-360 range
        blipAngle = (blipAngle + 360) % 360;
        sweepAngle = (sweepAngle + 360) % 360;

        // Ensure the blip is only fully visible when the sweep passes
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(sweepAngle, blipAngle));

        if (angleDifference < 5f) // Adjust for tighter or looser detection
        {
            blipImage.color = new Color(1f, 1f, 1f, 1f); // Fully visible
        }
        else
        {
            blipImage.color = new Color(1f, 1f, 1f, Mathf.Lerp(blipImage.color.a, 0f, fadeSpeed * Time.deltaTime));
        }
    }

    void RemoveOutOfRangeBlips(HashSet<GameObject> detectedItems)
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (var entry in activeBlips)
        {
            if (!detectedItems.Contains(entry.Key))
            {
                Destroy(entry.Value);
                toRemove.Add(entry.Key);
            }
        }

        foreach (var item in toRemove)
        {
            activeBlips.Remove(item);
        }
    }
}
