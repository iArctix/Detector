using UnityEngine;

public class Shovel : MonoBehaviour
{
    public SmoothTerrainDeformation terrainDeformation;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Left click to dig
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Dig at the position where the player clicked
                terrainDeformation.DigAtPosition(hit.point);
            }
        }
    }
}