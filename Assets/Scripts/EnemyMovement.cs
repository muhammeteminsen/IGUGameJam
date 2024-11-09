using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;        // Düþman hýzýný buradan kontrol edebilirsiniz
    public float normalSpeed = 5f;  // Normal hýz, donma veya yavaþlatma durumunda kullanýlýr
    private Transform player;       // Oyuncu hedefi
    private Vector3 direction;      // Düþman hareket yönü
    private float distanceToPlayer; // Oyuncuya olan mesafe

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;  // Oyuncuyu bul
        normalSpeed = speed;  // Baþlangýçta normalSpeed, speed ile eþit olacak
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    // Oyuncuya doðru hareket etme
    void MoveTowardsPlayer()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > 1f)  // Mesafe 1 birimden büyükse, oyuncuya doðru hareket et
        {
            direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // Düþman hýzýný yavaþlat
    public void ApplySlowEffect(float slowAmount, float duration)
    {
        speed *= slowAmount; // Hýzý yavaþlat
        Invoke(nameof(RestoreSpeed), duration);  // Hýzý eski haline getir
    }

    // Hýzý eski haline döndür
    private void RestoreSpeed()
    {
        speed = normalSpeed;  // Hýzý normalSpeed'e geri döndür
    }

    // Düþmaný dondur
    public void Freeze()
    {
        speed = 0;  // Hýzý sýfýrla
    }

    // Dondurmayý kaldýr
    public void Unfreeze()
    {
        speed = normalSpeed;

    }
}
