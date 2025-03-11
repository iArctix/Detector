using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemData itemData; // Reference to item data
    public bool wasCovered = false; // Set when terrain first covers it
    public bool canBeCollected = false; // Becomes true when uncovered
    public bool isUncovered = false; // True when no terrain is above

    private Rigidbody rb;
    private Collider itemCollider;
    public LayerMask terrainLayer; // Assign this to "Terrain" in the Inspector

    public int Quality { get; private set; } // Randomized item quality
    public int ActualPrice { get; private set; } // Price based on quality

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        itemCollider = GetComponent<Collider>();
        rb.useGravity = false; // Start with gravity disabled
        terrainLayer = LayerMask.GetMask("terrain");

        if (itemData != null)
        {
            Initialize(itemData);
        }
    }

    void Update()
    {
        bool terrainIsInside = CheckForTerrain();

        if (terrainIsInside)
        {
            wasCovered = true;
            isUncovered = false;
        }
        else if (wasCovered && !terrainIsInside) // Uncovered for the first time
        {
            isUncovered = true;
            EnableGravity();
        }
    }

    bool CheckForTerrain()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, itemCollider.bounds.extents.magnitude, terrainLayer);

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
        if (!canBeCollected)
        {
            rb.useGravity = true;
            canBeCollected = true;
            rb.isKinematic = false;

            itemCollider.isTrigger = false;
        }
    }

    public bool CanBeCollected()
    {
        return canBeCollected;
    }

    public void Initialize(ItemData data)
    {
        itemData = data;

        // Randomly determine item quality (1-100)
        Quality = Random.Range(1, 101);

        // Calculate the item's actual price based on quality
        ActualPrice = Mathf.RoundToInt(itemData.basePrice * (Quality / 100f));
    }
}

