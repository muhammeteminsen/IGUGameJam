using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    public Button skillButton1; // 1. buton
    public Button skillButton2; // 2. buton
    public Button skillButton3; // 3. buton

    private ManaSystem manaSystem; // Mana sistem referans�

    private void Start()
    {
        manaSystem = GetComponent<ManaSystem>();

        // Butonlara dinleyici ekleme
        skillButton1.onClick.AddListener(() => UseSkill(1));
        skillButton2.onClick.AddListener(() => UseSkill(2));
        skillButton3.onClick.AddListener(() => UseSkill(3));
    }

    private void Update()
    {
        // Klavye tu�lar� ile yetenek kullanma
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 1 tu�una bas�ld���nda
        {
            UseSkill(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) // 2 tu�una bas�ld���nda
        {
            UseSkill(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) // 3 tu�una bas�ld���nda
        {
            UseSkill(3);
        }
    }

    private void UseSkill(int skillNumber)
    {
        switch (skillNumber)
        {
            case 1:
                ActivateSpeedBoost();
                break;
            case 2:
                SlowEnemies();
                break;
            case 3:
                FreezeEnemies();
                break;
            default:
                Debug.Log("Ge�ersiz yetenek numaras�!");
                break;
        }
    }

    private void ActivateSpeedBoost()
    {
        if (manaSystem.currentMana >= 10) // Mana kontrol�
        {
            Debug.Log("H�z art��� yetene�i etkinle�tirildi!");
            manaSystem.currentMana -= 10; // Mana harcama
            // Burada h�z art��� i�levselli�ini ekleyin
        }
        else
        {
            Debug.Log("Yeterli mana yok!");
        }
    }

    private void SlowEnemies()
    {
        if (manaSystem.currentMana >= 20) // Mana kontrol�
        {
            Debug.Log("D��man yava�latma yetene�i etkinle�tirildi!");
            manaSystem.currentMana -= 20; // Mana harcama
            // Burada d��manlar� yava�latma i�levselli�ini ekleyin
        }
        else
        {
            Debug.Log("Yeterli mana yok!");
        }
    }

    private void FreezeEnemies()
    {
        if (manaSystem.currentMana >= 30) // Mana kontrol�
        {
            Debug.Log("D��man dondurma yetene�i etkinle�tirildi!");
            manaSystem.currentMana -= 30; // Mana harcama
            // Burada d��manlar� dondurma i�levselli�ini ekleyin
        }
        else
        {
            Debug.Log("Yeterli mana yok!");
        }
    }
}
