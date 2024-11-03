using UnityEngine;
using UnityEngine.UI;

public class SkillUIManager : MonoBehaviour
{
    public Button skillButton1;
    public Button skillButton2;
    public Button skillButton3;

    public Image skillIcon1;
    public Image skillIcon2;
    public Image skillIcon3;

    private ManaSystem manaSystem;

    private void Start()
    {
        manaSystem = GetComponent<ManaSystem>();

        // Baþlangýçta butonlarý ve ikonlarý pasif hale getirin
        skillButton1.interactable = false;
        skillButton2.interactable = false;
        skillButton3.interactable = false;

        // Butonlara týklanabilirlik ekleyin
        skillButton1.onClick.AddListener(ActivateSpeedBoost);
        skillButton2.onClick.AddListener(SlowEnemies);
        skillButton3.onClick.AddListener(FreezeEnemies);
    }

    private void Update()
    {
        // Mana yeterli olduðunda butonlarý aktif edin
        skillButton1.interactable = manaSystem.currentMana >= 10;
        skillButton2.interactable = manaSystem.currentMana >= 20;
        skillButton3.interactable = manaSystem.currentMana >= 30;

        // Mana dolunca ikonlarý göster
        skillIcon1.enabled = manaSystem.currentMana >= 10;
        skillIcon2.enabled = manaSystem.currentMana >= 20;
        skillIcon3.enabled = manaSystem.currentMana >= 30;
        Skills();
    }

    public void ActivateSpeedBoost()
    {
        if (manaSystem.currentMana >= 10)
        {
            manaSystem.currentMana -= 10;
            Debug.Log("Hýz artýþý etkin!");
            // Hýz artýþý iþlevini buraya ekleyin
        }
    }

    public void SlowEnemies()
    {
        if (manaSystem.currentMana >= 20)
        {
            manaSystem.currentMana -= 20;
            Debug.Log("Düþmanlar yavaþlatýldý!");
            // Düþmanlarý yavaþlatma iþlevini buraya ekleyin
        }
    }

    public void FreezeEnemies()
    {
        if (manaSystem.currentMana >= 30)
        {
            manaSystem.currentMana -= 30;
            Debug.Log("Düþmanlar donduruldu!");
            // Düþmanlarý dondurma iþlevini buraya ekleyin
        }
    }

    public void Skills()
    {
        if (Input.GetButtonDown("Fire1"))  // 1 tuþuna atanan giriþ
        {
            ActivateSpeedBoost();
        }
        if (Input.GetButtonDown("Fire2"))  // 2 tuþuna atanan giriþ
        {
            SlowEnemies();
        }
        if (Input.GetButtonDown("Fire3"))  // 3 tuþuna atanan giriþ
        {
            FreezeEnemies();
        }
    }
}