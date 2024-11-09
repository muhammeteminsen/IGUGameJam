using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;        // D��man h�z�n� buradan kontrol edebilirsiniz
    public float normalSpeed = 5f;  // Normal h�z, donma veya yava�latma durumunda kullan�l�r
    private Transform player;       // Oyuncu hedefi
    private Vector3 direction;      // D��man hareket y�n�
    private float distanceToPlayer; // Oyuncuya olan mesafe

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;  // Oyuncuyu bul
        normalSpeed = speed;  // Ba�lang��ta normalSpeed, speed ile e�it olacak
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    // Oyuncuya do�ru hareket etme
    void MoveTowardsPlayer()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > 1f)  // Mesafe 1 birimden b�y�kse, oyuncuya do�ru hareket et
        {
            direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // D��man h�z�n� yava�lat
    public void ApplySlowEffect(float slowAmount, float duration)
    {
        speed *= slowAmount; // H�z� yava�lat
        Invoke(nameof(RestoreSpeed), duration);  // H�z� eski haline getir
    }

    // H�z� eski haline d�nd�r
    private void RestoreSpeed()
    {
        speed = normalSpeed;  // H�z� normalSpeed'e geri d�nd�r
    }

    // D��man� dondur
    public void Freeze()
    {
        speed = 0;  // H�z� s�f�rla
    }

    // Dondurmay� kald�r
    public void Unfreeze()
    {
        speed = normalSpeed;

    }
}
