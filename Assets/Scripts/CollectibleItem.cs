using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public bool wasCovered = false;
    public bool canBeCollected = false;
    public bool isUncovered = false;
    private Rigidbody rb;
    private int groundCollisions = 0; // Track number of ground collisions

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Start with gravity disabled
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("terrain"))
        {
            wasCovered = true; // The item has been covered at least once
            groundCollisions++; // Increase collision count
            isUncovered = false; // Reset uncover status
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("terrain"))
        {
            groundCollisions--; // Reduce collision count
        }
    }

    void Update()
    {
        // If it was covered at least once and is no longer colliding with the ground, mark as uncovered
        if (wasCovered && groundCollisions <= 0)
        {
            isUncovered = true;
            EnableGravity();
        }
    }

    void EnableGravity()
    {
        if (!canBeCollected) // Only enable once
        {
            rb.useGravity = true; // Enable gravity
            canBeCollected = true; // Now the item can be collected
        }
    }

    public bool CanBeCollected()
    {
        return canBeCollected;
    }
}