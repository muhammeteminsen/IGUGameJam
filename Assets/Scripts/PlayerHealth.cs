using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;  // Oyuncunun ba�lang�� sa�l���

    // Oyuncu hasar ald���nda �a�r�lan fonksiyon
    public void TakeDamage(float damage)
    {
        health -= damage;  // Sa�l�k de�erini azalt�yoruz

        // Sa�l�k s�f�r�n alt�na d��erse (�l�rse)
        if (health <= 0f)
        {
            Die();  // Oyuncu �l�rse �lme fonksiyonunu �a��r�yoruz
        }
    }

    // Oyuncu �ld���nde yap�lacak i�lemler
    void Die()
    {
        Debug.Log("Player is dead!");
        // Buraya oyuncunun �ld��� zaman yap�lacak i�lemleri ekleyebilirsiniz (�rne�in animasyon, oyun biti�i vb.)
    }
}
