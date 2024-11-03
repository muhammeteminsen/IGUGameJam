using UnityEngine;

public class FreezeEnemies : MonoBehaviour
{
    public float freezeDuration = 5.0f; // Gücün süresi

    public void ActivateFreezeEnemies()
    {
        // Tüm düþmanlarý bul ve hýzlarýný sýfýrla
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyMovement>().speed = 0;
        }

        // Gücü belirli süre sonra devre dýþý býrak
        Invoke("DeactivateFreezeEnemies", freezeDuration);
    }

    private void DeactivateFreezeEnemies()
    {
        // Düþman hýzlarýný eski haline getir
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyMovement>().ResetSpeed(); // Orijinal hýza döndür
        }
    }
}

