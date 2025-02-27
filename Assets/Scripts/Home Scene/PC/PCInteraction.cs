using UnityEngine;

public class PCInteraction : MonoBehaviour
{
    public Camera playerCamera;         // Reference to the player's camera
    public Camera pcCamera;            // Reference to the PC camera
    public GameObject player;          // Reference to the player GameObject
    public GameObject pcUI;            // Reference to the PC UI (the screen interface the player can interact with)
    public GameObject pclight;         // Reference to the PC's light (if any)

    private bool playerInRange = false;

    public bool onpc = false;

    void Update()
    {
        // When the player is close to the PC and presses 'E', switch to the PC camera
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            EnterPC();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Detect when the player enters the trigger zone
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Detect when the player exits the trigger zone
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void EnterPC()
    {
        // Disable the player and player camera
        player.SetActive(false);
        playerCamera.gameObject.SetActive(false);
        playerCamera.enabled = false; // Disable player camera rendering

        // Enable the PC camera and the PC UI
        pcCamera.gameObject.SetActive(true);
        pcCamera.enabled = true; // Enable PC camera rendering
        pcUI.SetActive(true); // Show the PC interface UI

        // Optional: Lock mouse cursor when on PC screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pclight.SetActive(true);
    }

    public void ExitPC()
    {
        // Re-enable the player and player camera
        player.SetActive(true);
        playerCamera.gameObject.SetActive(true);
        playerCamera.enabled = true; // Enable player camera rendering

        // Disable the PC camera and PC UI
        pcCamera.gameObject.SetActive(false);
        pcCamera.enabled = false; // Disable PC camera rendering
        pcUI.SetActive(false); // Hide the PC interface UI

        // Optional: Lock mouse cursor back to normal when exiting PC
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pclight.SetActive(false);
    }
}
