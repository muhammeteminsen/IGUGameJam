using UnityEngine;
using UnityEngine.UI;

    public class ManaSystem : MonoBehaviour
{
    public Slider manaBar;
    public float maxMana = 100f;
    private float currentMana;
    public float manaRegenRate = 1f;
    public float killManaBonus = 5f;

    // Güçlerin mana maliyeti
    public float speedBoostManaCost = 20f;
    public float slowEnemiesManaCost = 30f;
    public float freezeEnemiesManaCost = 50f;

    public SpeedBoost speedBoost;  // SpeedBoost scripti
    public SlowEnemies slowEnemies; // SlowEnemies scripti
    public FreezeEnemies freezeEnemies; // FreezeEnemies scripti

    private void Start()
    {
        currentMana = 0;
        manaBar.maxValue = maxMana;
        manaBar.value = currentMana;
    }

    private void Update()
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
    }

    public void RegenerateMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
        manaBar.value = currentMana;
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
            manaBar.value = currentMana;
            return true;
        }
        return false;
    }
}

