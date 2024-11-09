using UnityEngine;

public enum EnemyType
{
    Normal,   // Normal d��man
    Medium,   // Orta seviye d��man
    Boss      // Boss d��man
}

public class EnemyDamage : MonoBehaviour
{
    public EnemyType enemyType;  // D��man�n tipi (Normal, Medium, Boss)
    public float damage;         // D��man�n verece�i hasar
    public float attackRange = 1f; // D��man�n sald�r� menzili
    public Transform player;      // Oyuncu referans�
    private PlayerHealth playerHealth;  // Oyuncunun sa�l�k script'i

    void Start()
    {
        // Player'�n sa�l�k script'ini al�yoruz
        playerHealth = player.GetComponent<PlayerHealth>();

        // D��man tipine g�re hasar belirliyoruz
        SetDamage();
    }

    void Update()
    {
        AttackPlayer();  // Oyuncuya sald�r�y� kontrol ediyoruz
    }

    // D��man tipine g�re hasar� ayarlama
    void SetDamage()
    {
        switch (enemyType)
        {
            case EnemyType.Normal:
                damage = 5f;  // Normal d��man 5 hasar verir
                break;
            case EnemyType.Medium:
                damage = 10f; // Medium d��man 10 hasar verir
                break;
            case EnemyType.Boss:
                damage = 20f; // Boss d��man 20 hasar verir
                break;
        }
    }

    // D��man oyuncuya sald�r� yap�p yapmad���n� kontrol etme
    void AttackPlayer()
    {
        // D��man ile oyuncu aras�ndaki mesafeyi kontrol etme
        float distance = Vector2.Distance(transform.position, player.position);

        // E�er mesafe, sald�r� menzilinden daha k���kse
        if (distance < attackRange)
        {
            // Oyuncuya hasar ver
            playerHealth.TakeDamage(damage);
        }
    }
}
