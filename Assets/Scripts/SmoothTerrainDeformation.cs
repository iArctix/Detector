using UnityEngine;

public class SmoothTerrainDeformation : MonoBehaviour
{
    public float terrainSize = 10f;            // Size of the terrain (10x10 for example)
    public int terrainResolution = 1000;         // Resolution of the terrain (how many vertices)
    public float digDepth = 0.5f;              // How deep the hole should go per dig
    public float digRadius = 0.5f;             // Radius of the digging area

    private Mesh terrainMesh;                  // The mesh of the terrain
    private Vector3[] terrainVertices;        // Vertices of the terrain
    private MeshCollider meshCollider;        // Collider for raycasting

    void Start()
    {
        terrainMesh = GetComponent<MeshFilter>().mesh;
        terrainVertices = terrainMesh.vertices;
        meshCollider = GetComponent<MeshCollider>();
    }

    void Update()
    {
        // Raycast to check where the player is digging
        if (Input.GetMouseButtonDown(0))  // Left click to dig
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Dig at the position where the player clicked
                DigAtPosition(hit.point);
            }
        }
    }

    // Digging function that modifies the terrain mesh
    public void DigAtPosition(Vector3 position)
    {
        // Convert world position to local position in the terrain
        Vector3 localPos = transform.InverseTransformPoint(position);

        // Find the corresponding vertex on the terrain grid
        int vertexX = Mathf.FloorToInt(localPos.x / (terrainSize / terrainResolution));
        int vertexZ = Mathf.FloorToInt(localPos.z / (terrainSize / terrainResolution));

        // Loop through the surrounding vertices in the radius
        for (int x = vertexX - Mathf.FloorToInt(digRadius); x < vertexX + Mathf.FloorToInt(digRadius); x++)
        {
            for (int z = vertexZ - Mathf.FloorToInt(digRadius); z < vertexZ + Mathf.FloorToInt(digRadius); z++)
            {
                // Ensure we're within bounds of the terrain
                if (x >= 0 && x < terrainResolution && z >= 0 && z < terrainResolution)
                {
                    // Calculate distance from the center of the dig area
                    float dist = Vector2.Distance(new Vector2(x, z), new Vector2(vertexX, vertexZ));

                    if (dist <= digRadius)
                    {
                        // Modify the height of the vertex within the dig radius
                        int index = x + z * terrainResolution;
                        terrainVertices[index].y -= digDepth * (1 - dist / digRadius);  // Gradual digging effect
                    }
                }
            }
        }

        // Update the terrain mesh with modified vertices
        terrainMesh.vertices = terrainVertices;
        terrainMesh.RecalculateNormals();  // Recalculate normals to make the lighting look correct
        meshCollider.sharedMesh = terrainMesh;  // Update the collider to match the new mesh
    }
}