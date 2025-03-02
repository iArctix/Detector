using UnityEngine;

public class Movement : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float detectorSpeed = 2f; // Slower speed for metal detector mode
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;
    public Transform cameraHolder;  // Child object for vertical camera rotation
    public ToolSwap toolSwap;       // Reference to ToolSwap script

    private Rigidbody rb;
    private float rotationX = 0f;
    private bool isGrounded = false;
    private bool detectorMode = false; // Tracks if metal detector is equipped

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents physics from affecting rotation

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleJump();

        // Check if the metal detector is equipped
        detectorMode = toolSwap.IsMetalDetectorEquipped();

        if (detectorMode)
        {
            LockCameraForDetector(); // Lock camera when metal detector is equipped
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate left/right (always allowed)
        transform.Rotate(Vector3.up * mouseX);

        if (!detectorMode)
        {
            // Regular vertical look
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);
            cameraHolder.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }
    }

    void LockCameraForDetector()
    {
        // Fix camera looking downward when using metal detector
        cameraHolder.localRotation = Quaternion.Euler(30f, 0f, 0f); // Tilt camera downward
        rotationX = 30f; // Lock vertical rotation
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float currentSpeed = detectorMode ? detectorSpeed : normalSpeed; // Adjust speed for detector mode

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        Vector3 newPosition = rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("terrain"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("terrain"))
        {
            isGrounded = false;
        }
    }
}
