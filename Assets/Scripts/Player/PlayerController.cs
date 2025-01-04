using GlobalSpace;
using PlayerSpace;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Movement Settings")]
    private Vector2 input;
    [SerializeField] private float speed;
    private Vector3 direction;
    private Vector3 forward, right;
    [SerializeField] private float smoothTime = 0.05f;
    private float currentVelocity;

    [Header("Look Settings")]
    public float mouseSensitivity = 2.0f;
    public float verticalLookLimit = 80.0f;
    private bool canLook;
    private float verticalRotation = 0.0f;

    private float gravity = -9.81f;
    private Vector3 velocity;
    public Transform cameraTransform;
    private CharacterController characterController;

    protected override void Awake()
    {
        base.Awake();
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        RedirectDirection.onChangeDirection += DisableMovement;
        RedirectDirection.onAllowMovement += EnableMovement;
    }

    private void OnDisable()
    {
        RedirectDirection.onChangeDirection -= DisableMovement;
        RedirectDirection.onAllowMovement -= EnableMovement;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        canLook = true;
        UpdateMovementVectors();
    }

    private void UpdateMovementVectors()
    {
        forward = cameraTransform.forward;
        right = cameraTransform.right;
        forward.y = right.y = 0;
        forward.Normalize();
        right.Normalize();
    }

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (canLook)
        {
            Look();
        }

        direction = (forward * input.y + right * input.x).normalized;

        if (characterController.isGrounded)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        Vector3 movement = direction * speed;
        characterController.Move((movement + velocity) * Time.deltaTime);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        UpdateMovementVectors();
    }

    public void MoveToPosition(Transform targetPos, float speed)
    {
        StartCoroutine(StartMoving(targetPos, speed));
    }

    IEnumerator StartMoving(Transform targetPos, float speed)
    {
        while (Vector3.Distance(transform.position, targetPos.position) > 0.3f)
        {
            Vector3 direction = (targetPos.position - transform.position).normalized;
            characterController.Move(direction * speed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("end");
        characterController.enabled = false;
    }

    public void ResetMovement()
    {
        characterController.enabled = true;
    }

    private void DisableMovement()
    {
        canLook = false;
    }

    private void EnableMovement()
    {
        canLook = true;
    }
}
