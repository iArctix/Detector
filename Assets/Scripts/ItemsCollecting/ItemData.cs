using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    public string itemName; // Item name (can be randomized later)
    public Sprite itemIcon; // 2D sprite for UI
    public GameObject itemModel; // 3D model prefab
    public Rarity rarity; // Rarity of the item
    public string information; // Description of item
    public int basePrice; // Base value of item

    [Range(1, 100)]
    public int quality; // Quality (1-100), randomized when spawned

    public int GetActualPrice()
    {
        // Price scaling based on quality (example: basePrice * quality percentage)
        return Mathf.RoundToInt(basePrice * (quality / 100f));
    }
}

// Enum for rarity levels
public enum Rarity { Common, Uncommon, Rare, Epic, Legendary }