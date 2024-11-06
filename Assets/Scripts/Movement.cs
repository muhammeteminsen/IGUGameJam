using UnityEngine;


public class Movement : MonoBehaviour
{
    [Header("Movement Variables")] 
    [SerializeField] public float movementSpeed = 15;
    [SerializeField] private float movementDeceleration = 5;
    [SerializeField] private float movementAcceleration = 1.5f;
    [SerializeField] private float velocityMaxValue = 10;
    
    [Header("Camera Variables")] 
    [SerializeField] private float mouseSensitivity = 3;
    [SerializeField] private float verticalLookMaxValue = 90f;
    [Header("Jump Variables")] 
    [SerializeField] private float jumpHeight = 3;
    [SerializeField] private float fallMultiplier = 2.5f;
    [Header("Sprint Variables")]
    [SerializeField] public float sprintSpeed = 25f;
    [SerializeField] private float sprintVelocityMaxValue = 8;
    [SerializeField] private float sprintAcceleration = 2.5f;
    [SerializeField] private float sprintDeceleration = 2.5f;
    [SerializeField] private float sprintFOV=100f;

    private float _xRotation;
    private float _defaultSpeed;
    private float _defaultVelocityMaxValue;
    private float _defaultFOV;
    private Camera _camera;
    private Transform _cameraHolder;
    private CharacterController _characterController;
    private Vector3 _velocity;
    private bool _isGrounded;
    
    private GunSystem _gunsystem;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
        if (_camera != null) 
            _cameraHolder = _camera.transform.parent;
        _gunsystem = GetComponent<GunSystem>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Physics.gravity = new Vector3(0, -9.81f, 0);
        _defaultSpeed = movementSpeed;
        _defaultVelocityMaxValue = velocityMaxValue;
        if (_camera != null)
            _defaultFOV = _camera.fieldOfView;
    }

    void Update()
    {
        CameraRotation();
        Move();
        Jump();
        Sprint();
    }

    private void Move()
    {
        _isGrounded = _characterController.isGrounded;
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

        _characterController.Move(move * movementSpeed * Time.deltaTime);

        _velocity.y += Physics.gravity.y * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);

        // Animator parameters
      //  bool isWalking = moveHorizontal != 0 || moveVertical != 0;
       // _gunsystem.GunAnimator.SetBool("Walk", isWalking);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        if (!_isGrounded)
        {
            _velocity.y += Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Sprint()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        if (isSprinting)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, sprintSpeed, sprintAcceleration * Time.deltaTime);
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, sprintFOV, sprintAcceleration * Time.deltaTime);
            _gunsystem.GunAnimator.SetBool("Run", true);
        }
        else
        {
            movementSpeed = _defaultSpeed;
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _defaultFOV, sprintDeceleration * Time.deltaTime);
            _gunsystem.GunAnimator.SetBool("Run", false);
        }
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -verticalLookMaxValue, verticalLookMaxValue);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + mouseX, 0);
        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}