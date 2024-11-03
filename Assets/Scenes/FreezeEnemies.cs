using UnityEngine;

public class FreezeEnemies : MonoBehaviour
{
    public float freezeDuration = 5.0f; // G�c�n s�resi

    public void ActivateFreezeEnemies()
    {
        // T�m d��manlar� bul ve h�zlar�n� s�f�rla
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyMovement>().speed = 0;
        }

        // G�c� belirli s�re sonra devre d��� b�rak
        Invoke("DeactivateFreezeEnemies", freezeDuration);
    }

    private void DeactivateFreezeEnemies()
    {
        // D��man h�zlar�n� eski haline getir
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyMovement>().ResetSpeed(); // Orijinal h�za d�nd�r
        }
    }
}

