using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class GridTerrain : MonoBehaviour
{
    public int width = 100;  // Width of the grid
    public int height = 100; // Height of the grid
    public float cellSize = 0.5f; // Size of each cell

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private MeshCollider meshCollider;
    private float[] originalHeights; // Store the original Y positions of vertices

    void Start()
    {
        mesh = new Mesh();
        meshCollider = GetComponent<MeshCollider>();
        CreateGridMesh();
    }

    void CreateGridMesh()
    {
        // Increase vertex density by 2x in both directions
        int vertexCount = (width * 2 + 1) * (height * 2 + 1);
        vertices = new Vector3[vertexCount];
        originalHeights = new float[vertexCount];  // Initialize array for original heights

        // Increase triangle density by 4x (2 triangles per subdivided square)
        int triangleCount = (width * 2) * (height * 2) * 6; // 6 indices per square
        triangles = new int[triangleCount];

        // Create vertices
        int v = 0;
        for (int z = 0; z <= height * 2; z++) // Loop over the finer grid
        {
            for (int x = 0; x <= width * 2; x++)
            {
                vertices[v] = new Vector3(x * cellSize / 2, 0, z * cellSize / 2); // Halve the cell size for finer detail
                originalHeights[v] = vertices[v].y;  // Store the original Y position
                v++;
            }
        }

        // Create triangles (each subdivided square will have 2 triangles)
        int t = 0;
        for (int z = 0; z < height * 2; z++) // Loop over finer grid cells
        {
            for (int x = 0; x < width * 2; x++)
            {
                int start = z * (width * 2 + 1) + x;

                // First triangle (lower left triangle)
                triangles[t] = start;
                triangles[t + 1] = start + width * 2 + 1;
                triangles[t + 2] = start + 1;

                // Second triangle (upper right triangle)
                triangles[t + 3] = start + 1;
                triangles[t + 4] = start + width * 2 + 1;
                triangles[t + 5] = start + width * 2 + 2;

                t += 6; // Move to the next triangle (6 indices per square)
            }
        }

        // Apply vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); // Recalculate normals for proper shading

        // Assign the mesh to the MeshFilter and MeshCollider
        GetComponent<MeshFilter>().mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    // Call this function from the ToolSwap script when digging
    public void DigHole(Vector3 point, float depth, float radius)
    {
        Vector3 localPoint = transform.InverseTransformPoint(point);

        for (int i = 0; i < vertices.Length; i++)
        {
            float distance = Vector3.Distance(localPoint, vertices[i]);

            if (distance < radius)
            {
                float falloff = Mathf.Pow(1 - (distance / radius), 2);
                float deformation = falloff * depth;

                vertices[i].y -= deformation; // Now can dig infinitely deep
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }
}
