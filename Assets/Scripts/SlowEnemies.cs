using UnityEngine;

public class SlowEnemies : MonoBehaviour
{
    public ManaSystem manaSystem;        // Mana Sistemi
    public float slowCost = 25f;         // Yavaþlatma maliyeti
    public float slowDuration = 5f;      // Yavaþlatma süresi
    public float slowMultiplier = 0.5f; // Düþman hýz çarpaný (normal hýzýn yarýsý olacak)

    private EnemyMovement[] enemies;     // Tüm düþmanlarý almak için
    private float[] originalSpeeds;      // Düþmanlarýn orijinal hýzlarý

    [System.Obsolete]
    void Start()
    {
        // Eðer Unity 2023 ve sonrasýnda iseniz FindObjectsByType kullanýn:
        // enemies = Object.FindObjectsByType<EnemyMovement>(FindObjectSortMode.None);

        // Eski sürümler için FindObjectsOfType kullanmaya devam edin
        enemies = FindObjectsOfType<EnemyMovement>();  // Bu eski fonksiyon hala geçerli
        originalSpeeds = new float[enemies.Length];   // Orijinal hýzlarý saklamak için dizi oluþturuyoruz

        // Düþmanlarýn orijinal hýzlarýný kaydediyoruz
        for (int i = 0; i < enemies.Length; i++)
        {
            originalSpeeds[i] = enemies[i].speed;  // Orijinal hýzlarý kaydet
        }
    }

    void Update()
    {
        // Eðer "3" tuþuna basýldýysa ve yeterli mana varsa, Slow Enemies'i aktive et
        if (Input.GetKeyDown(KeyCode.Alpha2) && manaSystem.UseMana(slowCost))
        {
            ActivateSlowEnemies();
        }
    }

    void ActivateSlowEnemies()
    {
        // Tüm düþmanlarý yavaþlatýyoruz
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].speed *= slowMultiplier; // Düþman hýzýný yavaþlat
        }
        Debug.Log("Düþmanlar yavaþlatýldý!");

        // Yavaþlatmanýn süresi bitince eski hýza dön
        Invoke("DeactivateSlowEnemies", slowDuration);
    }

    void DeactivateSlowEnemies()
    {
        // Tüm düþmanlarýn hýzýný eski haline getiriyoruz
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].speed = originalSpeeds[i]; // Düþman hýzýný eski haline getir
        }
        Debug.Log("Düþmanlar normale döndü!");
    }
}
