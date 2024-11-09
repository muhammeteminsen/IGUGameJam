using UnityEngine;

public class ManaSystem : MonoBehaviour
{
    public float maxMana = 100f;       // Maksimum mana
    public float currentMana = 100f;   // Ba�lang��ta mana 100
    public float manaRegenerationRate = 5f;  // Mana yenileme oran� (her saniye)

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
            currentMana -= amount;  // Mana harcan�r
            return true;  // Yeterli mana varsa true d�ner
        }
        return false;  // Yeterli mana yoksa false d�ner
    }
}
