using UnityEngine;
using System.Collections;

public class UpgradeBench : MonoBehaviour
{
    public GameObject toolSelectionUI;
    public GameObject upgradeUI;

    public Camera playerCamera;
    public Camera workbenchCamera;

    public GameObject player; // Reference to the player GameObject

    public ToolSelectionUI toolSelectionUIscript;

    public Transform selectionPosition; // Position for tool selection camera
    public Transform upgradePosition;   // Position for upgrade camera
    public float transitionSpeed = 1.5f;  // Speed for smooth transitions

    private bool playerInRange = false;
    private bool inUpgradeMode = false;

    void Start()
    {
        toolSelectionUI.SetActive(false);
        upgradeUI.SetActive(false);
        workbenchCamera.gameObject.SetActive(false);
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

        // Switch to workbench camera instantly (no smooth transition)
        playerCamera.gameObject.SetActive(false);
        workbenchCamera.gameObject.SetActive(true);

        // Enable Tool Selection UI
        toolSelectionUI.SetActive(true);
        upgradeUI.SetActive(false);

        inUpgradeMode = false;

        // Reset camera to selection position (instant snap)
        workbenchCamera.transform.position = selectionPosition.position;
        workbenchCamera.transform.rotation = selectionPosition.rotation;
    }

    public void SelectTool(GameObject tool)
    {
        // Hide Tool Selection UI and switch to Upgrade Camera
        toolSelectionUI.SetActive(false);
        upgradeUI.SetActive(true);

        inUpgradeMode = true;

        // Start the smooth camera transition to the upgrade position
        StartCoroutine(SmoothCameraTransition(workbenchCamera.transform, upgradePosition));
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

        // Disable workbench camera
        workbenchCamera.gameObject.SetActive(false);

        // Enable player camera
        playerCamera.gameObject.SetActive(true);

        // Reset UI state
        toolSelectionUIscript.deselectboth();

        inUpgradeMode = false;
    }

    // This is the method triggered by the UI button
    public void BackToSelect()
    {
        // Start smooth camera transition back to the selection position
        StartCoroutine(SmoothCameraTransition(workbenchCamera.transform, selectionPosition));

        // Enable tool selection UI
        toolSelectionUI.SetActive(true);
        upgradeUI.SetActive(false);

        inUpgradeMode = false;

        // Reset UI state
        toolSelectionUIscript.deselectboth();
    }

    IEnumerator SmoothCameraTransition(Transform currentCamera, Transform targetPosition)
    {
        // Smoothly transition the camera position and rotation
        float journeyLength = Vector3.Distance(currentCamera.position, targetPosition.position);
        float startTime = Time.time;

        while (Vector3.Distance(currentCamera.position, targetPosition.position) > 0.01f)
        {
            float distanceCovered = (Time.time - startTime) * transitionSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            currentCamera.position = Vector3.Lerp(currentCamera.position, targetPosition.position, fractionOfJourney);
            currentCamera.rotation = Quaternion.Lerp(currentCamera.rotation, targetPosition.rotation, fractionOfJourney);

            yield return null;
        }
    }
}
