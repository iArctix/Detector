using UnityEngine;

public class ToolSwap : MonoBehaviour
{
    public GameObject fist;           // Reference to the Fist (default tool)
    public GameObject metalDetector;  // Reference to the Metal Detector
    public GameObject shovel;         // Reference to the Shovel

    private GameObject currentTool;    // Keeps track of the equipped tool

    private void Start()
    {
        EquipTool(fist); // Start with Fist equipped
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) // Press 0 to equip Fist
        {
            EquipTool(fist);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Press 1 to equip Metal Detector
        {
            EquipTool(metalDetector);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) // Press 2 to equip Shovel
        {
            EquipTool(shovel);
        }
    }

    // Function to enable the selected tool and disable the others
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
}
