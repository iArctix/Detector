using UnityEngine;

public class UpgradeBench : MonoBehaviour
{
    public GameObject toolSelectionUI;
    public GameObject upgradeUI;

    public Camera playerCamera;
    public Camera workbenchCamera;
    public Camera upgradeCamera;

    public GameObject player; // Reference to the player GameObject

    private bool playerInRange = false;
    private bool inUpgradeMode = false;

    void Start()
    {
        toolSelectionUI.SetActive(false);
        upgradeUI.SetActive(false);
        workbenchCamera.gameObject.SetActive(false);
        upgradeCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !inUpgradeMode)
        {
            EnterWorkbench();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void EnterWorkbench()
    {
        // Disable the player
        if (player != null)
        {
            player.SetActive(false);
        }

        // Enable mouse cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Switch to workbench camera
        playerCamera.gameObject.SetActive(false);
        workbenchCamera.gameObject.SetActive(true);

        // Enable Tool Selection UI
        toolSelectionUI.SetActive(true);
    }

    public void SelectTool(GameObject tool)
    {
        // Hide Tool Selection UI, switch to Upgrade Camera
        toolSelectionUI.SetActive(false);
        workbenchCamera.gameObject.SetActive(false);
        upgradeCamera.gameObject.SetActive(true);
        upgradeUI.SetActive(true);

        inUpgradeMode = true;
    }

    public void ExitUpgradeMode()
    {
        // Re-enable the player
        if (player != null)
        {
            player.SetActive(true);
        }

        // Disable mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Hide all UI elements
        toolSelectionUI.SetActive(false);
        upgradeUI.SetActive(false);

        // Disable both workbench and upgrade cameras
        workbenchCamera.gameObject.SetActive(false);
        upgradeCamera.gameObject.SetActive(false);

        // Enable player camera
        playerCamera.gameObject.SetActive(true);

        inUpgradeMode = false;
    }
}
