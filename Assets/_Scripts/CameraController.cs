using System.Collections.Generic;
using UnityEngine;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;


    // This is a very nice camera controller I've created from using this youtube video: 
    // https://www.youtube.com/watch?v=9Vlgz6vshYA
    // that I've used from projects in the past.
public class CameraController : MonoBehaviour {

    [Header("Framing")]
    [SerializeField] private Camera _camera = null; 
    [SerializeField] private Transform _followTransform = null;
    [SerializeField] private Vector3 _framing = new Vector3(0,0,0);

    [Header("Distance")]

    [SerializeField] private float _zoomSpeed = 10f; 
    [SerializeField] private float _defaultDistance = 5f;
    [SerializeField] private float _minDistance = 0f;
    [SerializeField] private float _maxDistance = 10f;

    [Header("Rotation")]
    [SerializeField] private bool _invertX = false;
    [SerializeField] private bool _invertY = false;
    [SerializeField] private bool _invertScroll = false;
    [SerializeField] private float _rotationSharpness = 25f;
    [SerializeField] private float _defaultVerticalAngle = 20f;
    [SerializeField] [Range(-90,90)] private float _minVerticalAngle = -90f;
    [SerializeField] [Range(-90,90)] private float _maxVerticalAngle = 90f;
    [SerializeField] [Range(0.1f, 2)] private float _mouseSensitivity = 1;

    [Header("Obstructions")]
    [SerializeField] private float _checkRadius = 0.2f;
    [SerializeField] private LayerMask _obstructionLayers = -1;
    private List<Collider> _ignoreColliders = new List<Collider>();

    public Vector3 CameraPlanarDirection {get => _planarDirection;}
    //Private
    private PlayerInput playerInput;
    private Vector3 _planarDirection; //Camera's forward on x,z plane
    private float _targetDistance;
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private float _targetVerticalAngle;
    private Vector3 _newPosition;
    private Quaternion _newRotation;
    private float _mouseX;
    private float _mouseY;
    private float _zoom;



    // Methods


    private void Awake() {
        playerInput = new PlayerInput();

        playerInput.CameraControls.MouseXInput.started += OnMouseXInput;
        playerInput.CameraControls.MouseYInput.started += OnMouseYInput;
        playerInput.CameraControls.MouseScrollInput.started += OnMouseScrollInput;

        playerInput.CameraControls.MouseXInput.canceled += OnMouseXInput;
        playerInput.CameraControls.MouseYInput.canceled += OnMouseYInput;
        playerInput.CameraControls.MouseScrollInput.canceled += OnMouseScrollInput;

        playerInput.CameraControls.MouseXInput.performed += OnMouseXInput;
        playerInput.CameraControls.MouseYInput.performed += OnMouseYInput;
        playerInput.CameraControls.MouseScrollInput.performed += OnMouseScrollInput;
    }
    private void OnEnable() {
        playerInput.CameraControls.Enable();
    }

    private void OnDisable() {
        playerInput.CameraControls.Disable();
    }

    private void OnMouseXInput(Context c) {
        float xInput = c.ReadValue<float>();
        _mouseX = xInput * _mouseSensitivity;
        if (_invertX) {_mouseX *= -1f;}
    }

    private void OnMouseYInput(Context c) {
        float yInput = c.ReadValue<float>();
        _mouseY = yInput * _mouseSensitivity;
        if (_invertY) {_mouseY *= -1f;}
    }

    private void OnMouseScrollInput(Context c) {
        float scrollInput = c.ReadValue<float>();
        _zoom = scrollInput * _zoomSpeed /1200.0f;
        if (_invertScroll) {_zoom *= -1f;}
    }

    private void OnValidate() {
        _defaultDistance = Mathf.Clamp(_defaultDistance, _minDistance, _maxDistance);
        _defaultVerticalAngle = Mathf.Clamp(_defaultVerticalAngle, _minVerticalAngle, _maxVerticalAngle);
    }

    private void Start() {
        _ignoreColliders.AddRange(GetComponentsInChildren<Collider>());

        //Important
        _planarDirection = _followTransform.forward;

        //Calculate Targets
        _targetDistance = _defaultDistance;
        _targetVerticalAngle = _defaultVerticalAngle;
        _targetRotation = Quaternion.LookRotation(_planarDirection) * Quaternion.Euler(_targetVerticalAngle, 0, 0);
        _targetPosition = _followTransform.position - (_targetRotation * Vector3.forward) * _targetDistance;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        if (Cursor.lockState != CursorLockMode.Locked) {
            return;
        }
        Debug.Log(_zoom);

        Vector3 _focusPosition = _followTransform.position + _camera.transform.TransformDirection(_framing);

        _planarDirection = Quaternion.Euler(0, _mouseX, 0) * _planarDirection;
        _targetDistance = Mathf.Clamp(_targetDistance + _zoom, _minDistance, _maxDistance);
        _targetVerticalAngle = Mathf.Clamp(_targetVerticalAngle + _mouseY, _minVerticalAngle, _maxVerticalAngle);
        Debug.DrawLine(_camera.transform.position, _camera.transform.position + _planarDirection, Color.red);
        
        //Handle Obstructions (affects target distance)
        float _smallestDistance = _targetDistance;
        RaycastHit[] _hits = Physics.SphereCastAll(_focusPosition, _checkRadius, _targetRotation * -Vector3.forward, _targetDistance, _obstructionLayers);
        if (_hits.Length != 0) {
            foreach (RaycastHit hit in _hits) {
                if (!_ignoreColliders.Contains(hit.collider)) {
                    if (hit.distance < _smallestDistance) {
                        _smallestDistance = hit.distance;
                    }
                }
            }
        }

        //Final Targets
        _targetRotation = Quaternion.LookRotation(_planarDirection) * Quaternion.Euler(_targetVerticalAngle, 0, 0);
        _targetPosition = _focusPosition - (_targetRotation * Vector3.forward) * _smallestDistance;

        //Handle Smoothing 
        _newRotation = Quaternion.Slerp(_camera.transform.rotation, _targetRotation, Time.deltaTime * _rotationSharpness);
        _newPosition = Vector3.Lerp(_camera.transform.position, _targetPosition, Time.deltaTime * _rotationSharpness);
        //Apply
        _camera.transform.rotation = _newRotation;
        _camera.transform.position = _newPosition;
    }
}
