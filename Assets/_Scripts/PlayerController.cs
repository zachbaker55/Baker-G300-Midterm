using UnityEngine;
using UnityEngine.InputSystem;
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

    
    [SerializeField] private float _jumpHeight;
    public float JumpHeight {
        get { return _jumpHeight; }
        set { _jumpHeight = value; }
    }

    
    [SerializeField] private float _jumpTime;
    public float JumpTime {
        get { return _jumpTime; }
        set { _jumpTime = value; }
    }
    

    [SerializeField] private Player _player;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private Transform _bulletSpawn;


    // Fields
    private PlayerInput playerInput;
    private Camera mainCamera;
    private Vector3 currentMovement;
    private float gravity = -9.8f;
    private float jumpVelocity;
    private bool isMovementPressed;
    private bool isRunPressed = false;
    private bool isJumpPressed = false;
    private bool isJumping = false;

    // Components
    private CharacterController controller;
    


    // Methods
    private void Awake() {
        playerInput = new PlayerInput();
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        playerInput.CharacterControls.Move.started += OnInput;
        playerInput.CharacterControls.Move.canceled += OnInput;
        playerInput.CharacterControls.Move.performed += OnInput;
        
        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;

        playerInput.CharacterControls.Jump.started += OnJump;
        playerInput.CharacterControls.Jump.canceled += OnJump;  

        playerInput.CharacterControls.Shoot.started += OnShoot;   
    }

    private void Start() {
        float apex = JumpTime * 0.5f;
        gravity = (-2 * JumpHeight) / Mathf.Pow(apex, 2);
        jumpVelocity = (2 * JumpHeight) / apex;
    }

    private void Update() {
        DoMovement();
        DoRotation();
    }

    private void OnEnable() {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable() {
        playerInput.CharacterControls.Disable();
    }

    private void OnInput(Context c) {
        Vector2 movementInput = c.ReadValue<Vector2>();
        currentMovement.x = movementInput.x;
        currentMovement.z = movementInput.y;
        isMovementPressed = movementInput.x != 0 || movementInput.y != 0;
    }

    private void OnRun(Context c) {
        isRunPressed = c.ReadValueAsButton();
    }

    private void OnJump(Context c) {
        isJumpPressed = c.ReadValueAsButton();
    }

    private void OnShoot(Context c) {
        if (_player.BulletCount >= _player.MaxBulletCount || _player.AmmoAmount <= 0) {
            return;
        }
        GameObject newBullet = Instantiate(_bulletPrefab, _bulletSpawn.position, new Quaternion());
        Bullet bullet = newBullet.GetComponent<Bullet>();
        _player.BulletCount++;
        _player.AmmoAmount--;
        bullet.Init(_player, 1);
    }

    private void DoMovement() {
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = mainCamera.transform.right;
        right.y = 0;
        right = right.normalized;

        Vector3 verticalMovement = currentMovement.z * forward;
        Vector3 horizontalMovement = currentMovement.x * right;
        Vector3 cameraRelativeMovement = verticalMovement + horizontalMovement;

        cameraRelativeMovement.y = currentMovement.y;

        if (isRunPressed) {
            controller.Move(cameraRelativeMovement * Time.deltaTime * RunSpeed);
        } else controller.Move(cameraRelativeMovement * Time.deltaTime * WalkSpeed);

        if (!controller.isGrounded) {
            currentMovement.y += gravity * Time.deltaTime;
        } else {
            currentMovement.y = -0.05f;
        }


        if (!isJumping && controller.isGrounded && isJumpPressed) {
            isJumping = true;
            currentMovement.y = jumpVelocity;
        } else if (!isJumpPressed && isJumping && controller.isGrounded) {
            isJumping = false;
        }

    }

    private void DoRotation() {
        Vector3 positionToLookAt = mainCamera.transform.forward;
        positionToLookAt.y = 0;

        if (isMovementPressed) {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = targetRotation;
        }

    }



}
