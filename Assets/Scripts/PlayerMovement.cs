using UnityEngine;
using UnityEngine.Serialization;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")] [SerializeField]
    public float movementSpeed = 15;
    public bool isSpring;
    [SerializeField] private float movementDeceleration = 5;
    [SerializeField] private float movementAcceleration = 1.5f;
    [SerializeField] private float velocityMaxValue = 10;

    [Header("Camera Variables")] [SerializeField]
    private float mouseSensitivity = 3;

    [SerializeField] private float verticalLookMaxValue = 90f;

    [Header("Jump Variables")] [SerializeField]
    private float jumpHeight = 3;
    [SerializeField] private float fallMultiplier = 2.5f;

    [Header("Sprint Variables")] [SerializeField]
    private float sprintSpeed = 25f;
    [SerializeField] private float sprintFOV=100f;
    [SerializeField] private float sprintVelocityMaxValue = 8;
    [SerializeField] private float sprintAcceleration = 2.5f;
    [SerializeField] private float sprintDeceleration = 2.5f;


    private float _xRotation;
    [HideInInspector] public float defaultSpeed;
    private float _defaultSprintFOV;
    private float _defaultVelocityMaxValue;
    private Vector3 _velocity;
    private Camera _camera;
    private Transform _cameraHolder;
    private CharacterController _characterController;
    private Animator _animatorGun;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
        if (_camera != null)
        {
            _cameraHolder = _camera.transform.parent;
            _defaultSprintFOV = _camera.fieldOfView;
        }
          
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        defaultSpeed = movementSpeed;
        _defaultVelocityMaxValue = velocityMaxValue;
        _animatorGun = GetComponentInChildren<Animator>();
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
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.right * moveHorizontal + transform.forward * moveVertical;
        moveDirection = moveDirection.normalized * movementSpeed;

        if (_characterController.isGrounded)
        {
            _velocity.x = Mathf.Lerp(_velocity.x, moveDirection.x, movementAcceleration * Time.deltaTime);
            _velocity.z = Mathf.Lerp(_velocity.z, moveDirection.z, movementAcceleration * Time.deltaTime);
            if (moveHorizontal == 0 && moveVertical == 0)
            {
                _animatorGun.SetBool("Idle", true);
                _velocity.x = Mathf.Lerp(_velocity.x, 0, movementDeceleration * Time.deltaTime);
                _velocity.z = Mathf.Lerp(_velocity.z, 0, movementDeceleration * Time.deltaTime);
            }
        }
        else
        {
            _velocity.x = moveDirection.x;
            _velocity.z = moveDirection.z;
        }

        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (_characterController.isGrounded && Input.GetKey(KeyCode.Space))
        {
            _velocity.y = jumpHeight;
        }
        else if (!_characterController.isGrounded)
        {
            _velocity.y += Physics.gravity.y * fallMultiplier * Time.deltaTime;
        }
    }

    private void Sprint()
    {
        isSpring = Input.GetKey(KeyCode.LeftShift);
        if (isSpring)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, sprintSpeed, sprintAcceleration * Time.deltaTime);
            velocityMaxValue = sprintVelocityMaxValue;
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView,sprintFOV, sprintAcceleration* Time.deltaTime);
            _animatorGun.SetBool("Run", true);
        }
        else
        {
            movementSpeed = defaultSpeed;
            velocityMaxValue = Mathf.Lerp(velocityMaxValue, _defaultVelocityMaxValue,
                sprintDeceleration * Time.deltaTime);
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView,_defaultSprintFOV, sprintDeceleration* Time.deltaTime);
            _animatorGun.SetBool("Run", false);
        }
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -verticalLookMaxValue, verticalLookMaxValue);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + mouseX, 0);
        _cameraHolder.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}