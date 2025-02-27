using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;
    public Transform cameraHolder;  // Assign a child object for vertical camera rotation

    private Rigidbody rb;
    private float rotationX = 0f;
    private bool isGrounded = false;

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
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate player body left/right (Rigidbody rotation for smoother physics)
        Quaternion newRotation = Quaternion.Euler(0f, mouseX, 0f) * rb.rotation;
        rb.MoveRotation(newRotation);

        // Rotate camera up/down
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        cameraHolder.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        Vector3 newPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
