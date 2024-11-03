using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    public Button skillButton1; // 1. buton
    public Button skillButton2; // 2. buton
    public Button skillButton3; // 3. buton

    private ManaSystem manaSystem; // Mana sistem referansý

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
        // Klavye tuþlarý ile yetenek kullanma
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 1 tuþuna basýldýðýnda
        {
            UseSkill(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) // 2 tuþuna basýldýðýnda
        {
            UseSkill(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) // 3 tuþuna basýldýðýnda
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
                Debug.Log("Geçersiz yetenek numarasý!");
                break;
        }
    }

    private void ActivateSpeedBoost()
    {
        if (manaSystem.currentMana >= 10) // Mana kontrolü
        {
            Debug.Log("Hýz artýþý yeteneði etkinleþtirildi!");
            manaSystem.currentMana -= 10; // Mana harcama
            // Burada hýz artýþý iþlevselliðini ekleyin
        }
        else
        {
            Debug.Log("Yeterli mana yok!");
        }
    }

    private void SlowEnemies()
    {
        if (manaSystem.currentMana >= 20) // Mana kontrolü
        {
            Debug.Log("Düþman yavaþlatma yeteneði etkinleþtirildi!");
            manaSystem.currentMana -= 20; // Mana harcama
            // Burada düþmanlarý yavaþlatma iþlevselliðini ekleyin
        }
        else
        {
            Debug.Log("Yeterli mana yok!");
        }
    }

    private void FreezeEnemies()
    {
        if (manaSystem.currentMana >= 30) // Mana kontrolü
        {
            Debug.Log("Düþman dondurma yeteneði etkinleþtirildi!");
            manaSystem.currentMana -= 30; // Mana harcama
            // Burada düþmanlarý dondurma iþlevselliðini ekleyin
        }
        else
        {
            Debug.Log("Yeterli mana yok!");
        }
    }
}
