using UnityEngine;

public class SlowEnemies : MonoBehaviour
{
    public ManaSystem manaSystem;        // Mana Sistemi
    public float slowCost = 25f;         // Yava�latma maliyeti
    public float slowDuration = 5f;      // Yava�latma s�resi
    public float slowMultiplier = 0.5f; // D��man h�z �arpan� (normal h�z�n yar�s� olacak)

    private EnemyMovement[] enemies;     // T�m d��manlar� almak i�in
    private float[] originalSpeeds;      // D��manlar�n orijinal h�zlar�

    [System.Obsolete]
    void Start()
    {
        // E�er Unity 2023 ve sonras�nda iseniz FindObjectsByType kullan�n:
        // enemies = Object.FindObjectsByType<EnemyMovement>(FindObjectSortMode.None);

        // Eski s�r�mler i�in FindObjectsOfType kullanmaya devam edin
        enemies = FindObjectsOfType<EnemyMovement>();  // Bu eski fonksiyon hala ge�erli
        originalSpeeds = new float[enemies.Length];   // Orijinal h�zlar� saklamak i�in dizi olu�turuyoruz

        // D��manlar�n orijinal h�zlar�n� kaydediyoruz
        for (int i = 0; i < enemies.Length; i++)
        {
            originalSpeeds[i] = enemies[i].speed;  // Orijinal h�zlar� kaydet
        }
    }

    void Update()
    {
        // E�er "3" tu�una bas�ld�ysa ve yeterli mana varsa, Slow Enemies'i aktive et
        if (Input.GetKeyDown(KeyCode.Alpha2) && manaSystem.UseMana(slowCost))
        {
            ActivateSlowEnemies();
        }
    }

    void ActivateSlowEnemies()
    {
        // T�m d��manlar� yava�lat�yoruz
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].speed *= slowMultiplier; // D��man h�z�n� yava�lat
        }
        Debug.Log("D��manlar yava�lat�ld�!");

        // Yava�latman�n s�resi bitince eski h�za d�n
        Invoke("DeactivateSlowEnemies", slowDuration);
    }

    void DeactivateSlowEnemies()
    {
        // T�m d��manlar�n h�z�n� eski haline getiriyoruz
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].speed = originalSpeeds[i]; // D��man h�z�n� eski haline getir
        }
        Debug.Log("D��manlar normale d�nd�!");
    }
}
