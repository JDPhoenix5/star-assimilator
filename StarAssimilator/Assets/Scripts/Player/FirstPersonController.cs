using Unity.VisualScripting;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;

    [Header("Look Parameters")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float upDownLookRange = 80f;

    [Header("References")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerInputHandler playerInputHandler;

    [Header("Camera Bob and Sway")]
    private float camTilt;
    public float camTiltAmount = 10f;
    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.015f;
    [SerializeField, Range(0, 30f)] private float frequency = 10.0f;

    private float toggleSpeed = 3.0f;
    private Vector3 startPos;

    private Vector3 currentMovement;
    private float verticalRotation;

    private float CurrentSpeed => walkSpeed * (playerInputHandler.IsSprinting ? sprintMultiplier : 1f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private Vector3 CalculateWorldDirection()
    {
        Vector3 inputDirection = new Vector3(playerInputHandler.MovementInput.x, 0f, playerInputHandler.MovementInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        return worldDirection.normalized;
    }

    private void HandleMovement()
    {
        Vector3 worldDirection = CalculateWorldDirection();
        currentMovement.x = worldDirection.x * CurrentSpeed;
        currentMovement.z = worldDirection.z * CurrentSpeed;

        characterController.Move(currentMovement * Time.deltaTime);
    }

    private void ApplyHorizontalRotation(float rotationAmount)
    {
        transform.Rotate(0, rotationAmount, 0);
    }
    private void ApplyVerticalRotation(float rotationAmount)
    {
        verticalRotation = Mathf.Clamp(verticalRotation - rotationAmount, -upDownLookRange, upDownLookRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
    private void ApplySwayRotation()
    {
        camTilt = Mathf.Lerp(camTilt, playerInputHandler.MovementInput.x * camTiltAmount, Time.deltaTime * 5f);
        mainCamera.transform.localRotation = Quaternion.Euler(mainCamera.transform.localRotation.eulerAngles.x, mainCamera.transform.localRotation.eulerAngles.y, -camTilt);
    }
    private void ApplyCameraBob()
    {
        if (playerInputHandler.IsWalking && playerInputHandler.IsSprinting)
        {
            float bobbingAmount = Mathf.Sin(Time.time * (frequency * 1.5f)) * amplitude;
            mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, startPos.y + bobbingAmount, mainCamera.transform.localPosition.z);
        }
        else if (playerInputHandler.IsWalking)
        {
            float bobbingAmount = Mathf.Sin(Time.time * frequency) * (amplitude * 2);
            mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, startPos.y + bobbingAmount, mainCamera.transform.localPosition.z);
        }
        else
        {
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, startPos, Time.deltaTime * 5f);
        }
    }

    private void HandleRotation()
    {
        float mouseXRotation = playerInputHandler.RotationInput.x * mouseSensitivity;
        float mouseYRotation = playerInputHandler.RotationInput.y * mouseSensitivity;

        ApplyHorizontalRotation(mouseXRotation);
        ApplyVerticalRotation(mouseYRotation);
        ApplySwayRotation();
        ApplyCameraBob();
    }
}