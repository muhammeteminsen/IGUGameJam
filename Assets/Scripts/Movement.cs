using UnityEngine;


public class Movement : MonoBehaviour
{
    [Header("Movement Variables")] 
    [SerializeField] private float movementSpeed = 15;
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
    [SerializeField] private float sprintSpeed = 25f;
    [SerializeField] private float sprintVelocityMaxValue = 8;
    [SerializeField] private float sprintAcceleration = 2.5f;
    [SerializeField] private float sprintDeceleration = 2.5f;
    [SerializeField] private float sprintFOV=100f;

    private Rigidbody _rb;
    private float _xRotation;
    private float _defaultSpeed;
    private float _defaultVelocityMaxValue;
    private float _defaultFOV;
    private Camera _camera;
    private bool _isGrounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Physics.gravity = new Vector3(0, -9.81f, 0);
        _defaultSpeed = movementSpeed;
        _defaultVelocityMaxValue = velocityMaxValue;
        if (_camera != null)
            _defaultFOV=_camera.fieldOfView;
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
        float moveHorizontal = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float moveVertical = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        _rb.linearVelocity += transform.TransformDirection(movement * (movementSpeed * movementAcceleration));

        if (moveHorizontal == 0 && moveVertical == 0)
            _rb.linearVelocity = Vector3.Lerp(_rb.linearVelocity, Vector3.zero, movementDeceleration * Time.deltaTime);
        float clampedVelocity = Mathf.Clamp(_rb.linearVelocity.magnitude, 0, velocityMaxValue);
        _rb.linearVelocity = _rb.linearVelocity.normalized * clampedVelocity;
    }

    private void Jump()
    {
        
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, jumpHeight, _rb.linearVelocity.z);
        }

        if (!_isGrounded)
        {
            _rb.linearVelocity += Vector3.up * (Physics.gravity.y * fallMultiplier * Time.deltaTime);
        }
    }

    private void Sprint()
    {
        bool isSpring = Input.GetKey(KeyCode.LeftShift);
        if (isSpring)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, sprintSpeed, sprintAcceleration * Time.deltaTime);
            _camera.fieldOfView=Mathf.Lerp(_camera.fieldOfView, sprintFOV, sprintAcceleration * Time.deltaTime);                                                                                                                                                                                                                          
            velocityMaxValue = sprintVelocityMaxValue;  
        }       

        else
        {
            movementSpeed = _defaultSpeed;
            velocityMaxValue = Mathf.Lerp(velocityMaxValue, _defaultVelocityMaxValue,
                sprintDeceleration * Time.deltaTime);
            _camera.fieldOfView = _camera.fieldOfView=Mathf.Lerp(_camera.fieldOfView, _defaultFOV, 
                sprintDeceleration * Time.deltaTime);
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

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}