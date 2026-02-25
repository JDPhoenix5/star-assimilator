using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name Reference")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string movement = "Movement";
    [SerializeField] private string rotation = "Rotation";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string interact = "Interact";
    [SerializeField] private string drop = "Drop";

    private InputAction movementAction;
    private InputAction rotationAction;
    private InputAction sprintAction;
    private InputAction interactAction;
    private InputAction dropAction;

    public Vector2 MovementInput { get; private set; }
    public Vector2 RotationInput { get; private set; }

    public bool IsSprinting { get; private set; }
    public bool IsWalking { get; private set; }
    public bool Interact {  get; private set; }
    public bool Drop { get; private set; }

    private void Awake()
    {
        InputActionMap mapReference = playerControls.FindActionMap(actionMapName);

        movementAction = mapReference.FindAction(movement);
        rotationAction = mapReference.FindAction(rotation);
        sprintAction = mapReference.FindAction(sprint);
        interactAction = mapReference.FindAction(interact);
        dropAction = mapReference.FindAction(drop);

        SubscribeActionValuesToInputEvents();
    }

    private void SubscribeActionValuesToInputEvents()
    {
        movementAction.performed += inputInfo =>
        {
            MovementInput = inputInfo.ReadValue<Vector2>();
            IsWalking = true;
        };

        movementAction.canceled += inputInfo =>
        {
            MovementInput = Vector2.zero;
            IsWalking = false;
        };

        rotationAction.performed += inputInfo => RotationInput = inputInfo.ReadValue<Vector2>();
        rotationAction.canceled += inputInfo => RotationInput = Vector2.zero;

        sprintAction.performed += inputInfo => IsSprinting = true;
        sprintAction.canceled += inputInfo => IsSprinting = false;

        interactAction.performed += inputInfo => Interact = true;
        interactAction.canceled += inputInfo => Interact = false; 

        dropAction.performed += inputInfo => Drop = true;
        dropAction.canceled += inputInfo => Drop = false;

    }
    private void OnEnable()
    {
        playerControls.FindActionMap(actionMapName).Enable();
    }
    private void OnDisable()
    {
        playerControls.FindActionMap(actionMapName).Disable();
    }
}
