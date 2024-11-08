using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // Oyuncunun maksimum sa�l���
    private float currentHealth;    // Oyuncunun mevcut sa�l���

    void Start()
    {
        currentHealth = maxHealth; // Ba�lang��ta maksimum sa�l�k ile ba�la
    }

    // Hasar al�nd���nda �a�r�lacak fonksiyon
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;  // Sa�l��� azalt
        Debug.Log("Oyuncu hasar ald�! Kalan sa�l�k: " + currentHealth);

        // E�er sa�l�k 0 veya daha d���kse, oyuncuyu �ld�r
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Oyuncu �ld�, burada �l�m animasyonu veya ekran mesaj� ekleyebilirsiniz
        Debug.Log("Oyuncu �ld�!");
        // �ld���nde oyuncu yok olmal� (�rne�in, destroy et)
        Destroy(gameObject);
    }
}
