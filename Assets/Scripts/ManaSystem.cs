using UnityEngine;
using UnityEngine.UI;

    public class ManaSystem : MonoBehaviour
{
    public Slider Slider ;
    public float maxMana = 100f;
    public float currentMana;
    public float manaRegenRate = 1f;
    public float killManaBonus = 5f;

    // Güçlerin mana maliyeti
    public float speedBoostManaCost = 20f;
    public float slowEnemiesManaCost = 30f;
    public float freezeEnemiesManaCost = 50f;

    public SpeedBoost speedBoost;  // SpeedBoost scripti
    public SlowEnemies slowEnemies; // SlowEnemies scripti
    public FreezeEnemies freezeEnemies; // FreezeEnemies scripti

    public void Start()
    {
        currentMana = 20;
        Slider.maxValue = maxMana;
        Slider.value = currentMana;
    }

    public void Update()
    {
        // Zamanla mana yenilenmesi
        RegenerateMana(Time.deltaTime * manaRegenRate);

        // Güçleri tuşlara atama
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateSpeedBoost();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateSlowEnemies();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateFreezeEnemies();
        }
        else if(Input.GetKeyDown(KeyCode.U))
        {
            RegenerateMana(50);
        }
    }

    public void RegenerateMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
        Slider.value = currentMana;
    }

    public void OnEnemyKilled()
    {
        RegenerateMana(killManaBonus);
    }

    // Güçleri etkinleştiren ve mana kontrol eden fonksiyonlar
    private void ActivateSpeedBoost()
    {
        if (UseMana(speedBoostManaCost))
        {
            speedBoost.ActivateSpeedBoost();
        }
    }

    private void ActivateSlowEnemies()
    {
        if (UseMana(slowEnemiesManaCost))
        {
            slowEnemies.ActivateSlowEnemies();
        }
    }

    private void ActivateFreezeEnemies()
    {
        if (UseMana(freezeEnemiesManaCost))
        {
            freezeEnemies.ActivateFreezeEnemies();
        }
    }

    public bool UseMana(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            Slider.value = currentMana;
            return true;
        }
        return false;
    }
}

