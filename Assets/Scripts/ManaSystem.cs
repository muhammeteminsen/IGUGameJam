using UnityEngine;

public class ManaSystem : MonoBehaviour
{
    public float maxMana = 100f;       // Maksimum mana
    public float currentMana = 100f;   // Baþlangýçta mana 100
    public float manaRegenerationRate = 5f;  // Mana yenileme oraný (her saniye)

    void Update()
    {
        RegenerateMana();
    }

    // Mana yenileme fonksiyonu
    private void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana = Mathf.Min(currentMana + manaRegenerationRate * Time.deltaTime, maxMana);
        }
    }

    // Mana harcama fonksiyonu
    public bool UseMana(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;  // Mana harcanýr
            return true;  // Yeterli mana varsa true döner
        }
        return false;  // Yeterli mana yoksa false döner
    }
}
