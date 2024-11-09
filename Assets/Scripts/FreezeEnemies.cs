using UnityEngine;

public class FreezeEnemies : MonoBehaviour
{
    public ManaSystem manaSystem;     // Mana sistemi
    public float freezeCost = 30f;    // Dondurma maliyeti
    public float freezeDuration = 5f; // Dondurma s�resi
    public bool isFreeze = false;     // Dondurma durumu

    private EnemyMovement enemyMovement; // D��man hareketi

    void Start()
    {
        // EnemyMovement component'ini al�yoruz (D��man hareketini kontrol etmek i�in)
        enemyMovement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        // Tu�a bas�ld���nda ve yeterli mana varsa dondurmay� aktive et
        if (Input.GetKeyDown(KeyCode.Alpha2) && manaSystem.UseMana(freezeCost))
        {
            ActivateFreeze();
        }
    }

    void ActivateFreeze()
    {
        if (!isFreeze)
        {
            isFreeze = true;  // Dondurma etkinle�tirildi
            enemyMovement.enabled = false;  // D��man hareketi durduruluyor (freeze i�lemi)
            Debug.Log("D��manlar donduruldu!");

            // Dondurman�n s�resi bitince i�lemi sonland�r
            Invoke("DeactivateFreeze", freezeDuration);
        }
    }

    void DeactivateFreeze()
    {
        isFreeze = false;  // Dondurma etkisi sona erdi
        enemyMovement.enabled = true;   // D��man hareketi tekrar aktif
        Debug.Log("Dondurma etkisi sona erdi!");
    }
}
