using UnityEngine;

public class Shovel : MonoBehaviour
{
    public Camera playerCamera;   // Reference to the player camera
    public float raycastDistance = 10f; // Distance to check for terrain

    private GridTerrain gridTerrain; // Reference to the TerrainDeformation script

    void Start()
    {
        // Find the TerrainDeformation script attached to the terrain
        gridTerrain = FindObjectOfType<GridTerrain>();
    }

    void Update()
    {
        // If the player presses the dig button (left-click or a specific key)
        if (Input.GetMouseButtonDown(0)) // Left mouse button for digging
        {
            // Perform a raycast to detect where the player is looking on the terrain
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                // Check if the raycast hit the terrain
                if (hit.collider != null)
                {
                    // Call the DigAtPoint method from the TerrainDeformation script to deform the terrain
                    gridTerrain.DigHole(hit.point, 0.5f, 1f); ;
                }
            }
        }
    }
}