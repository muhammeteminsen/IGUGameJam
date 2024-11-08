using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;       // Hedef olarak alýnacak oyuncu
    public float speed = 3.0f;     // Düþmanýn hareket hýzý
    public float stoppingDistance = 1.5f; // Oyuncuya yaklaþma mesafesi

    private float originalSpeed;   // Hýzý dondurma ve eski haline döndürme için saklar

    private void Start()
    {
        originalSpeed = speed; // Orijinal hýzý kaydeder
        // Eðer sahnede bir oyuncu yoksa onu bulur
        if (player == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        // Eðer bir oyuncu varsa ve mesafe çarpýþma mesafesinden büyükse
        if (player != null && Vector3.Distance(transform.position, player.position) > stoppingDistance)
        {
            // Oyuncuya doðru hareket et
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // Hýzý sýfýrlamak için kullanýlýr
    public void Freeze()
    {
        speed = 0;
    }

    // Eski hýza döndürmek için kullanýlýr
    public void ResetSpeed()
    {
        speed = originalSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Eðer düþman oyuncuya çarptýysa
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
