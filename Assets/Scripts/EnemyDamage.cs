using UnityEngine;

public enum EnemyType
{
    Normal,   // Normal düþman
    Medium,   // Orta seviye düþman
    Boss      // Boss düþman
}

public class EnemyDamage : MonoBehaviour
{
    public EnemyType enemyType;  // Düþmanýn tipi (Normal, Medium, Boss)
    public float damage;         // Düþmanýn vereceði hasar
    public float attackRange = 1f; // Düþmanýn saldýrý menzili
    public Transform player;      // Oyuncu referansý
    private PlayerHealth playerHealth;  // Oyuncunun saðlýk script'i

    void Start()
    {
        // Player'ýn saðlýk script'ini alýyoruz
        playerHealth = player.GetComponent<PlayerHealth>();

        // Düþman tipine göre hasar belirliyoruz
        SetDamage();
    }

    void Update()
    {
        AttackPlayer();  // Oyuncuya saldýrýyý kontrol ediyoruz
    }

    // Düþman tipine göre hasarý ayarlama
    void SetDamage()
    {
        switch (enemyType)
        {
            case EnemyType.Normal:
                damage = 5f;  // Normal düþman 5 hasar verir
                break;
            case EnemyType.Medium:
                damage = 10f; // Medium düþman 10 hasar verir
                break;
            case EnemyType.Boss:
                damage = 20f; // Boss düþman 20 hasar verir
                break;
        }
    }

    // Düþman oyuncuya saldýrý yapýp yapmadýðýný kontrol etme
    void AttackPlayer()
    {
        // Düþman ile oyuncu arasýndaki mesafeyi kontrol etme
        float distance = Vector2.Distance(transform.position, player.position);

        // Eðer mesafe, saldýrý menzilinden daha küçükse
        if (distance < attackRange)
        {
            // Oyuncuya hasar ver
            playerHealth.TakeDamage(damage);
        }
    }
}
