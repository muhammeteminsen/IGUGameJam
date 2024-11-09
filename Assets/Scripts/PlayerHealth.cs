using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;  // Oyuncunun baþlangýç saðlýðý

    // Oyuncu hasar aldýðýnda çaðrýlan fonksiyon
    public void TakeDamage(float damage)
    {
        health -= damage;  // Saðlýk deðerini azaltýyoruz

        // Saðlýk sýfýrýn altýna düþerse (ölürse)
        if (health <= 0f)
        {
            Die();  // Oyuncu ölürse ölme fonksiyonunu çaðýrýyoruz
        }
    }

    // Oyuncu öldüðünde yapýlacak iþlemler
    void Die()
    {
        Debug.Log("Player is dead!");
        // Buraya oyuncunun öldüðü zaman yapýlacak iþlemleri ekleyebilirsiniz (örneðin animasyon, oyun bitiþi vb.)
    }
}
