using UnityEngine;

namespace PlayerSpace
{

    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float movementSpeed = 5.0f;
        public float gravity = -9.81f;
        public float jumpHeight = 2.0f;

        [Header("Look Settings")]
        public float mouseSensitivity = 2.0f;
        public float verticalLookLimit = 80.0f;

        public Transform cameraTransform; // Assign the camera manually in the Inspector

        private CharacterController controller;
        private Vector3 moveDirection = Vector3.zero;
        private float verticalRotation = 0.0f;
        private float verticalVelocity = 0.0f;

        void Start()
        {
            controller = GetComponent<CharacterController>();

            // Lock cursor at start
            Cursor.lockState = CursorLockMode.Locked;

            // Assign the camera transform if not assigned
            if (cameraTransform == null)
            {
                cameraTransform = Camera.main.transform;
            }
        }

        void Update()
        {
            // Handle player movement
            MovePlayer();

            // Handle player look
            Look();

            // Handle jumping
            if (Input.GetButtonDown("Jump") && controller.isGrounded)
            {
                Jump();
            }

            // Apply gravity
            ApplyGravity();

            // Move the controller
            controller.Move(moveDirection * Time.deltaTime);
        }

        void MovePlayer()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Calculate movement direction
            Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;
            moveDirection = move.normalized * movementSpeed;
        }

        void Look()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Rotate the player based on horizontal mouse movement
            transform.Rotate(Vector3.up * mouseX);

            // Rotate the camera vertically based on vertical mouse movement
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);
            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }

        void Jump()
        {
            verticalVelocity = Mathf.Sqrt(2 * jumpHeight * -gravity);
        }

        void ApplyGravity()
        {
            if (controller.isGrounded && verticalVelocity < 0)
            {
                verticalVelocity = -1f; // Ensures gravity doesn't accumulate when grounded
            }
            else
            {
                verticalVelocity += gravity * Time.deltaTime;
            }

            moveDirection.y = verticalVelocity;
        }
    }
}