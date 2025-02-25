using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public bool wasCovered = false; // Set when terrain first covers it
    public bool canBeCollected = false; // Becomes true when uncovered
    public bool isUncovered = false; // True when no terrain is above

    private Rigidbody rb;
    private Collider itemCollider; // Reference to the object's trigger collider
    public LayerMask terrainLayer; // Assign this to "Terrain" in the Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        itemCollider = GetComponent<Collider>();
        rb.useGravity = false; // Start with gravity disabled
    }

    void Update()
    {
        // Check if terrain is still inside the trigger area
        bool terrainIsInside = CheckForTerrain();

        if (terrainIsInside)
        {
            wasCovered = true; // Mark that it was covered at least once
            isUncovered = false; // Reset uncover state
        }
        else if (wasCovered && !terrainIsInside) // If terrain is gone and it was covered before
        {
            isUncovered = true;
            EnableGravity();
        }
    }

    bool CheckForTerrain()
    {
        // Get all colliders overlapping this object
        Collider[] colliders = Physics.OverlapSphere(transform.position, itemCollider.bounds.extents.magnitude, terrainLayer);

        // Check if any of the colliders belong to the terrain
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("terrain"))
            {
                return true;
            }
        }
        return false;
    }

    void EnableGravity()
    {
        if (!canBeCollected) // Only enable once
        {
            rb.useGravity = true; // Enable gravity
            canBeCollected = true; // Now the item can be collected
            rb.isKinematic = false;

            Collider col = GetComponent<Collider>();
            col.isTrigger = false;
        }
    }

    public bool CanBeCollected()
    {
        return canBeCollected;
    }
}
