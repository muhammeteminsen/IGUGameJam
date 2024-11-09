using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public ManaSystem manaSystem;       // Mana Sistemi
    public float speedBoostCost = 20f;  // Hýz artýþý maliyeti
    public float speedBoostDuration = 5f; // Hýz artýþý süresi
    public float speedMultiplier = 2f;  // Hýz çarpaný (normal hýzýn 2 katý olacak)

    private float originalSpeed;        // Orijinal hýz
    private PlayerMovement playerMovement; // Oyuncu hareketi

    void Start()
    {
        // PlayerMovement script'ini alýyoruz
        playerMovement = GetComponent<PlayerMovement>();
        originalSpeed = playerMovement.moveSpeed;  // Orijinal hýz
    }

    void Update()
    {
        // Eðer "1" tuþuna basýldýysa ve yeterli mana varsa, Speed Boost'u aktive et
        if (Input.GetKeyDown(KeyCode.Alpha1) && manaSystem.UseMana(speedBoostCost))
        {
            ActivateSpeedBoost();
        }
    }

    void ActivateSpeedBoost()
    {
        playerMovement.moveSpeed = originalSpeed * speedMultiplier;  // Hýz artýþý
        Debug.Log("Hýz arttý!");

        // Hýz artýþýnýn süresi bitince eski hýza dön
        Invoke("DeactivateSpeedBoost", speedBoostDuration);
    }

    void DeactivateSpeedBoost()
    {
        playerMovement.moveSpeed = originalSpeed;  // Hýzý eski haline getir
        Debug.Log("Hýz normale döndü!");
    }
}
