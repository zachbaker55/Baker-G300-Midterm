using UnityEngine;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

public class PlayerController : MonoBehaviour {

    // Properties
    [SerializeField] private float _walkSpeed;
    public float WalkSpeed {
        get { return _walkSpeed; }
        set { _walkSpeed = value; }
    }

    [SerializeField] private float _runSpeed;
    public float RunSpeed {
        get { return _runSpeed; }
        set { _runSpeed = value; }
    }

    // Fields
    private PlayerInput playerInput;
    private Vector2 movementInput;
    private Vector3 currentMovement;
    private bool isMovementPressed;

    // Components
    private CharacterController controller;
    


    // Methods - generic
    private void Awake() {
        playerInput = new PlayerInput();
        controller = GetComponent<CharacterController>();

        playerInput.CharacterControls.Move.started += OnInput;
        playerInput.CharacterControls.Move.canceled += OnInput;
        playerInput.CharacterControls.Move.performed += OnInput;
    }

    private void Update() {
        controller.Move(currentMovement * Time.deltaTime * WalkSpeed);
    }

    private void OnEnable() {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable() {
        playerInput.CharacterControls.Disable();
    }

    private void OnInput(Context c) {
        movementInput = c.ReadValue<Vector2>();
        currentMovement.x = movementInput.x;
        currentMovement.z = movementInput.y;
        isMovementPressed = !(movementInput.x == 0 && movementInput.y == 0);
    }



}
