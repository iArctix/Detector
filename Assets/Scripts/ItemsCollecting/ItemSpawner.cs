using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemData[] itemsToSpawn; // Array of ScriptableObject items
    public int itemCount = 10; // Number of items to spawn
    public BoxCollider spawnArea; // Assign this in the Inspector

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        if (spawnArea == null)
        {
            Debug.LogError("No BoxCollider assigned for spawning area!");
            return;
        }

        for (int i = 0; i < itemCount; i++)
        {
            Vector3 spawnPosition = GetRandomPositionInBox();

            // Select a random item
            ItemData selectedItem = itemsToSpawn[Random.Range(0, itemsToSpawn.Length)];

            // Instantiate the item's 3D model
            GameObject spawnedItem = Instantiate(selectedItem.itemModel, spawnPosition, Quaternion.identity);

            // Add the CollectibleItem component and assign data
            CollectibleItem collectible = spawnedItem.AddComponent<CollectibleItem>();
            collectible.Initialize(selectedItem);
        }
    }

    Vector3 GetRandomPositionInBox()
    {
        Bounds bounds = spawnArea.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}