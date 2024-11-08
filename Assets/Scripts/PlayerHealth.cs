using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // Oyuncunun maksimum saðlýðý
    private float currentHealth;    // Oyuncunun mevcut saðlýðý

    void Start()
    {
        currentHealth = maxHealth; // Baþlangýçta maksimum saðlýk ile baþla
    }

    // Hasar alýndýðýnda çaðrýlacak fonksiyon
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;  // Saðlýðý azalt
        Debug.Log("Oyuncu hasar aldý! Kalan saðlýk: " + currentHealth);

        // Eðer saðlýk 0 veya daha düþükse, oyuncuyu öldür
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Oyuncu öldü, burada ölüm animasyonu veya ekran mesajý ekleyebilirsiniz
        Debug.Log("Oyuncu öldü!");
        // Öldüðünde oyuncu yok olmalý (örneðin, destroy et)
        Destroy(gameObject);
    }
}
