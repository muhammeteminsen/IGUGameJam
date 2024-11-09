using UnityEngine;

public class FreezeEnemies : MonoBehaviour
{
    public ManaSystem manaSystem;     // Mana sistemi
    public float freezeCost = 30f;    // Dondurma maliyeti
    public float freezeDuration = 5f; // Dondurma süresi
    public bool isFreeze = false;     // Dondurma durumu

    private EnemyMovement enemyMovement; // Düþman hareketi

    void Start()
    {
        // EnemyMovement component'ini alýyoruz (Düþman hareketini kontrol etmek için)
        enemyMovement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        // Tuþa basýldýðýnda ve yeterli mana varsa dondurmayý aktive et
        if (Input.GetKeyDown(KeyCode.Alpha2) && manaSystem.UseMana(freezeCost))
        {
            ActivateFreeze();
        }
    }

    void ActivateFreeze()
    {
        if (!isFreeze)
        {
            isFreeze = true;  // Dondurma etkinleþtirildi
            enemyMovement.enabled = false;  // Düþman hareketi durduruluyor (freeze iþlemi)
            Debug.Log("Düþmanlar donduruldu!");

            // Dondurmanýn süresi bitince iþlemi sonlandýr
            Invoke("DeactivateFreeze", freezeDuration);
        }
    }

    void DeactivateFreeze()
    {
        isFreeze = false;  // Dondurma etkisi sona erdi
        enemyMovement.enabled = true;   // Düþman hareketi tekrar aktif
        Debug.Log("Dondurma etkisi sona erdi!");
    }
}
