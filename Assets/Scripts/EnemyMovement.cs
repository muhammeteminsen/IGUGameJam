using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;       // Hedef olarak al�nacak oyuncu
    public float speed = 3.0f;     // D��man�n hareket h�z�
    public float stoppingDistance = 1.5f; // Oyuncuya yakla�ma mesafesi

    private float originalSpeed;   // H�z� dondurma ve eski haline d�nd�rme i�in saklar

    private void Start()
    {
        originalSpeed = speed; // Orijinal h�z� kaydeder
        // E�er sahnede bir oyuncu yoksa onu bulur
        if (player == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        // E�er bir oyuncu varsa ve mesafe �arp��ma mesafesinden b�y�kse
        if (player != null && Vector3.Distance(transform.position, player.position) > stoppingDistance)
        {
            // Oyuncuya do�ru hareket et
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // H�z� s�f�rlamak i�in kullan�l�r
    public void Freeze()
    {
        speed = 0;
    }

    // Eski h�za d�nd�rmek i�in kullan�l�r
    public void ResetSpeed()
    {
        speed = originalSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        // E�er d��man oyuncuya �arpt�ysa
        if (other.CompareTag("Player"))
        {
            // Oyuncuya hasar ver
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // 5 hasar ver
            }
        }
    }
}
