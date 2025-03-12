using UnityEngine;

public class StationInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public Camera stationCamera;
    public GameObject player;
    public GameObject stationUI;
    public InventoryUIManager inventoryUIManager; // ? Reference to Inventory UI Manager

    private bool playerInRange = false;
    private bool isUsingStation = false;
    public bool isStorageStation = false; // ? Add this flag to differentiate stations

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isUsingStation)
        {
            EnterStation();
        }
        else if (isUsingStation && Input.GetKeyDown(KeyCode.Escape)) // Exit when pressing ESC
        {
            ExitStation();
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

    void EnterStation()
    {
        isUsingStation = true;

        player.SetActive(false);
        playerCamera.gameObject.SetActive(false);
        playerCamera.enabled = false;

        stationCamera.gameObject.SetActive(true);
        stationCamera.enabled = true;
        stationUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // ? Show inventory if it's a storage station
        if (isStorageStation && inventoryUIManager != null)
        {
            inventoryUIManager.inventoryPanel.SetActive(true);
            inventoryUIManager.PopulateInventory(); // Ensure it updates
        }
    }

    public void ExitStation()
    {
        isUsingStation = false;

        player.SetActive(true);
        playerCamera.gameObject.SetActive(true);
        playerCamera.enabled = true;

        stationCamera.gameObject.SetActive(false);
        stationCamera.enabled = false;
        stationUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // ? Hide inventory when leaving storage station
        if (isStorageStation && inventoryUIManager != null)
        {
            inventoryUIManager.inventoryPanel.SetActive(false);
            inventoryUIManager.ClearSpawnedItem(); // Remove any displayed items
        }
    }
}
