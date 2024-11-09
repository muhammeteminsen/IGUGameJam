using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public ManaSystem manaSystem;       // Mana Sistemi
    public float speedBoostCost = 20f;  // H�z art��� maliyeti
    public float speedBoostDuration = 5f; // H�z art��� s�resi
    public float speedMultiplier = 2f;  // H�z �arpan� (normal h�z�n 2 kat� olacak)

    private float originalSpeed;        // Orijinal h�z
    private PlayerMovement playerMovement; // Oyuncu hareketi

    void Start()
    {
        // PlayerMovement script'ini al�yoruz
        playerMovement = GetComponent<PlayerMovement>();
        originalSpeed = playerMovement.moveSpeed;  // Orijinal h�z
    }

    void Update()
    {
        // E�er "1" tu�una bas�ld�ysa ve yeterli mana varsa, Speed Boost'u aktive et
        if (Input.GetKeyDown(KeyCode.Alpha1) && manaSystem.UseMana(speedBoostCost))
        {
            ActivateSpeedBoost();
        }
    }

    void ActivateSpeedBoost()
    {
        playerMovement.moveSpeed = originalSpeed * speedMultiplier;  // H�z art���
        Debug.Log("H�z artt�!");

        // H�z art���n�n s�resi bitince eski h�za d�n
        Invoke("DeactivateSpeedBoost", speedBoostDuration);
    }

    void DeactivateSpeedBoost()
    {
        playerMovement.moveSpeed = originalSpeed;  // H�z� eski haline getir
        Debug.Log("H�z normale d�nd�!");
    }
}
