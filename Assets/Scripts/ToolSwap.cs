using UnityEngine;

public class ToolSwap : MonoBehaviour
{
    public GameObject fist;          // Default tool (always available)
    public GameObject metalDetector; // Metal detector
    public GameObject shovel;        // Shovel

    private GameObject currentTool;  // Tracks the currently equipped tool

    private void Start()
    {
        EquipTool(fist); // Start with fists equipped
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) // Press 0 to equip fists
        {
            EquipTool(fist);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) // Press 1 to equip metal detector
        {
            EquipTool(metalDetector);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // Press 2 to equip shovel
        {
            EquipTool(shovel);
        }
    }

    void EquipTool(GameObject toolToEquip)
    {
        // Disable all tools first
        fist.SetActive(false);
        metalDetector.SetActive(false);
        shovel.SetActive(false);

        // Enable the selected tool
        toolToEquip.SetActive(true);
        currentTool = toolToEquip;
    }

    // Check if the metal detector is equipped (for movement script)
    public bool IsMetalDetectorEquipped()
    {
        return currentTool == metalDetector;
    }
}
