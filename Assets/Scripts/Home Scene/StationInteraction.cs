using UnityEngine;

public class StationInteraction : MonoBehaviour
{
    public Camera playerCamera;   // Player's main camera
    public Camera stationCamera;  // Camera for this specific station
    public GameObject player;     // The player object
    public GameObject stationUI;  // UI for this station

    private bool playerInRange = false;
    private bool isUsingStation = false;

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

        // Disable player movement & camera
        player.SetActive(false);
        playerCamera.gameObject.SetActive(false);
        playerCamera.enabled = false;

        // Enable station camera & UI
        stationCamera.gameObject.SetActive(true);
        stationCamera.enabled = true;
        stationUI.SetActive(true);

        // Unlock cursor for UI interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitStation()
    {
        isUsingStation = false;

        // Re-enable player movement & camera
        player.SetActive(true);
        playerCamera.gameObject.SetActive(true);
        playerCamera.enabled = true;

        // Disable station camera & UI
        stationCamera.gameObject.SetActive(false);
        stationCamera.enabled = false;
        stationUI.SetActive(false);
        // Lock cursor back to game mode
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}