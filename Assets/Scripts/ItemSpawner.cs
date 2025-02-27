using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemsToSpawn; // Array of item prefabs
    public int itemCount = 10; // Number of items to spawn
    public BoxCollider spawnArea; // Assign this in the Inspector
    public float maxDepth = -5f; // Maximum depth relative to terrain surface

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

            // Instantiate a random item at the position
            GameObject itemPrefab = itemsToSpawn[Random.Range(0, itemsToSpawn.Length)];
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPositionInBox()
    {
        Bounds bounds = spawnArea.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y); // Full box height
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }

}
