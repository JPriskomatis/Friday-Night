using GlobalSpace;
using PlayerSpace;
using System.Collections;
using Unity.Cinemachine;
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
    [SerializeField] private bool canLook;
    private float verticalRotation = 0.0f;

    [Header("Cinemachine Settings")]
    public CinemachineCamera virtualCamera; // Reference to the Cinemachine Virtual Camera
    private CinemachineBasicMultiChannelPerlin noise; // Reference to the Noise component

    [Header("Audio Settings")]
    [SerializeField] private AudioSource footstepAudioSource;
    [SerializeField] private AudioClip footstepClip;

    private float footstepTimer = 0f;
    [SerializeField] private float footstepInterval = 0.5f; // Adjust to match the player's step speed

    private float gravity = -9.81f;
    private Vector3 velocity;
    public Transform cameraTransform;
    private CharacterController characterController;

    private Vector3 savedCameraPosition;
    private Quaternion savedCameraRotation;


    public bool canMove;

    protected override void Awake()
    {
        base.Awake();
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        RedirectDirection.onChangeDirection += DisableCameraMovement;
        RedirectDirection.onAllowMovement += EnableCaneraMovement;

        VoiceButtonsSetting.OnPause += PauseGame;
        VoiceButtonsSetting.OnResume += ResumeGame;
    }

    private void OnDisable()
    {
        RedirectDirection.onChangeDirection -= DisableCameraMovement;
        RedirectDirection.onAllowMovement -= EnableCaneraMovement;

        VoiceButtonsSetting.OnPause -= PauseGame;
        VoiceButtonsSetting.OnResume -= ResumeGame;
    }

    private void Start()
    {
        canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        canLook = true;
        UpdateMovementVectors();
        noise = virtualCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();

    }

    private void PauseGame()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //DisableCameraMovement();
        StopMovement();
        canMove = false;

        Time.timeScale = 0;

    }

    private void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //EnableCaneraMovement();
        ResetMovement();
        canMove = true;

        Time.timeScale = 1;

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

    public void StopCameraNoice()
    {
        ApplyNoise(0f, 0f);
    }
    public void ResetCameraNoise()
    {
        ApplyNoise(1f, 1f);
    }
    void ApplyNoise(float amplitude, float frequency)
    {
        if (noise != null)
        {
            noise.AmplitudeGain = amplitude;  // Set noise intensity
            noise.FrequencyGain = frequency;  // Set noise frequency (speed of noise)
        }
    }
    private void Update()
    {
        if (characterController.isGrounded)
        {
            Debug.Log("grounded");
        }
        else
        {
            Debug.Log("Not grounded");

        }
        if (canLook)
        {
            Look();
        } else
        {
            return;
        }

        direction = (forward * input.y + right * input.x).normalized;
        if (direction.magnitude > 0.1f) // Moving on the ground
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                PlayFootstepSound();
                footstepTimer = footstepInterval; // Reset timer
            }
        }
        else
        {
            footstepTimer = 0f; // Reset timer if not moving
        }
        if (characterController.isGrounded)
        {
            velocity.y = -0.1f; // Slight downward force to keep grounded
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }


        Vector3 movement = direction * speed;
        if (characterController.enabled || canMove)
        {
            characterController.Move((movement + velocity) * Time.deltaTime);
        }




    }
    private void PlayFootstepSound()
    {
        if (footstepAudioSource != null && footstepClip != null)
        {
            footstepAudioSource.PlayOneShot(footstepClip);  // Play a single footstep sound
        }
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
    public void StopMovement()
    {
        characterController.enabled = false;
    }

    public void DisableCameraMovement()
    {
        GetCameraPos();
        canLook = false;

        cameraTransform.localRotation = savedCameraRotation;
        cameraTransform.localPosition = savedCameraPosition;

        virtualCamera.enabled = false;
        
    }
    public void GetCameraPos()
    {
        savedCameraPosition = cameraTransform.localPosition;
        savedCameraRotation = cameraTransform.localRotation;
        Debug.Log(savedCameraPosition);
    }
    public void ResetCameraPos()
    {
        Debug.Log("sdc");
        cameraTransform.localPosition = savedCameraPosition; // Restore position
        cameraTransform.localRotation = savedCameraRotation;
        virtualCamera.gameObject.transform.localPosition = savedCameraPosition;
    }

    public void EnableCaneraMovement()
    {
        canLook = true;
        ResetCameraPos();

        virtualCamera.enabled = true;

    }
}
