using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private Rigidbody rb;
    private bool isUncovered = false;
    private bool canBeCollected = false;
    private float checkRadius = 0.5f; // Adjust based on object size
    private GridTerrain terrain; // Reference to terrain

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Disable physics initially
        terrain = FindObjectOfType<GridTerrain>(); // Find terrain in the scene
    }

    void Update()
    {
        if (!isUncovered)
        {
            CheckIfUncovered();
        }
    }

    void CheckIfUncovered()
    {
        Vector3 itemPosition = transform.position;
        float terrainHeight = terrain.GetTerrainHeightAt(itemPosition);

        // Check if the lowest point of the object is above the terrain
        if (itemPosition.y > terrainHeight)
        {
            isUncovered = true;
            rb.isKinematic = false; // Enable gravity
            canBeCollected = true;
        }
    }

    public bool CanBeCollected()
    {
        return canBeCollected;
    }
}
