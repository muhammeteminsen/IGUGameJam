using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;           // Hareket h�z�
    public float jumpForce = 5f;           // Z�plama kuvveti
    public float mouseSensitivity = 100f;  // Fare hassasiyeti

    private CharacterController controller; // Karakter kontrolc� bile�eni
    private Vector3 velocity;               // H�z vekt�r�
    private float xRotation = 0f;           // Dikey d�nme a��s�
    private bool isGrounded;                // Zemin kontrol�

    public Camera playerCamera;             // Oyuncu kameras�

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Cursor'u kilitle
    }

    void Update()
    {
        MovePlayer();
        MouseLook();
    }

    void MovePlayer()
    {
        // Zeminle temas kontrol� (CharacterController��n isGrounded �zelli�ini kullanarak)
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Yere de�iyorsa d��ey h�z� s�f�rla
        }

        // W, A, S, D ile hareket
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Z�plama
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }

        // Y�kseklik hareketi (yer�ekimi uygulama)
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void MouseLook()
    {
        // Fare hareketleri
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Dikey d�n��� s�n�rla
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
