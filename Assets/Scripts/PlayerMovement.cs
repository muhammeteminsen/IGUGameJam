using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;           // Hareket hýzý
    public float jumpForce = 5f;           // Zýplama kuvveti
    public float mouseSensitivity = 100f;  // Fare hassasiyeti

    private CharacterController controller; // Karakter kontrolcü bileþeni
    private Vector3 velocity;               // Hýz vektörü
    private float xRotation = 0f;           // Dikey dönme açýsý
    private bool isGrounded;                // Zemin kontrolü

    public Camera playerCamera;             // Oyuncu kamerasý

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
        // Zeminle temas kontrolü (CharacterController’ýn isGrounded özelliðini kullanarak)
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Yere deðiyorsa düþey hýzý sýfýrla
        }

        // W, A, S, D ile hareket
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Zýplama
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }

        // Yükseklik hareketi (yerçekimi uygulama)
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void MouseLook()
    {
        // Fare hareketleri
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Dikey dönüþü sýnýrla
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
