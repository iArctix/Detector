using UnityEngine;

public class ToolSwap : MonoBehaviour
{
    public GameObject metalDetector; // Reference to the metal detector
    public GameObject shovel;        // Reference to the shovel
    public Camera playerCamera;      // Reference to the camera (for raycasting)
    public LayerMask diggableLayer;  // Layer for diggable terrain (set to terrain layer)

    private void Update()
    {
        // Equip Metal Detector
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Press 1 to equip metal detector
        {
            EquipTool(metalDetector, shovel);
        }

        // Equip Shovel
        if (Input.GetKeyDown(KeyCode.Alpha2)) // Press 2 to equip shovel
        {
            EquipTool(shovel, metalDetector);
        }

       
    }
    // Function to enable the selected tool and disable the other
    void EquipTool(GameObject toolToEquip, GameObject toolToDisable)
    {
        toolToEquip.SetActive(true);   // Enable the tool to equip
        toolToDisable.SetActive(false); // Disable the other tool
    }


}
