using UnityEngine;

public class ToolFollow : MonoBehaviour
{
    public Transform playerCamera; // Assign the player's camera transform
    public Vector3 offset = new Vector3(0.5f, -0.5f, 1f); // Adjust position relative to camera
    public float rotationSpeed = 10f; // Smoothing for rotation

    private void Update()
    {
        if (gameObject.activeSelf) // Only update if the shovel is equipped
        {
            FollowCamera();
        }
    }

    void FollowCamera()
    {
        // Position the shovel relative to the camera
        transform.position = playerCamera.position + playerCamera.forward * offset.z
                             + playerCamera.right * offset.x
                             + playerCamera.up * offset.y;

        // Smoothly rotate the shovel to match camera rotation
        Quaternion targetRotation = Quaternion.LookRotation(playerCamera.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}