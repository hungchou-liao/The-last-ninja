using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class NewBehaviourScript : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float runSpeed = 8.0f;
    public float gravity = -11.81f;
    public float minJumpHeight = 2.0f;
    public float maxJumpHeight = 4.0f;
    public float groundedGravity = -1f; // Small gravity to keep the player grounded on slopes

    public Transform cameraTransform; // Reference to the camera

    private CharacterController controller;
    private float verticalVelocity;
    private bool isGrounded;
    private bool isJumping;
    private float jumpTimer;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpTimer = 0;

        // Tweak slope limit and step offset for smoother slope handling
        controller.slopeLimit = 45f; // Max angle player can walk on
        controller.stepOffset = 0.4f; // Max step height player can climb over
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is grounded using CharacterController's built-in isGrounded property
        isGrounded = controller.isGrounded;

        // Get movement input (WASD or arrow keys)
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Get the forward and right vectors relative to the camera's rotation
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Zero out the Y axis to prevent movement in the vertical direction (XZ-plane only)
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // Normalize the forward and right vectors
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the player's movement direction relative to the camera
        Vector3 moveDirection = cameraRight * horizontalInput + cameraForward * verticalInput;

        // Handle jumping logic
        if (isGrounded)
        {
            // Apply small downward force to keep player grounded, especially on slopes
            if (verticalVelocity < 0)
            {
                verticalVelocity = groundedGravity; // Use small gravity to stay grounded
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                verticalVelocity = Mathf.Sqrt(-2f * gravity * minJumpHeight); // Apply jump force
                jumpTimer = 0; // Reset jump timer
            }
        }
        else
        {
            // Apply gravity when in the air
            verticalVelocity += gravity * Time.deltaTime;

            // Handle extended jumping (holding space)
            if (isJumping && Input.GetKey(KeyCode.Space))
            {
                jumpTimer += Time.deltaTime;
                if (jumpTimer >= (maxJumpHeight - minJumpHeight) / Mathf.Sqrt(-2f * gravity * minJumpHeight) || !Input.GetKey(KeyCode.Space))
                {
                    isJumping = false; // Stop jumping when max height or space is released
                }
            }
        }

        // Handle running (Left Shift to run)
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Combine movement direction and vertical velocity (gravity)
        Vector3 velocity = moveDirection.normalized * speed + Vector3.up * verticalVelocity;

        // Move the player using CharacterController
        controller.Move(velocity * Time.deltaTime);

        // Rotate the player to face the direction of movement if there's any movement input
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime); // Smooth rotation
        }
    }
}