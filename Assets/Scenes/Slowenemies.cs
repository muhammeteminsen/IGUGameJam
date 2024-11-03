using UnityEngine;

public class SlowEnemies : MonoBehaviour
{
    public float slowDuration = 5.0f; // G�c�n s�resi
    public float enemySpeedMultiplier = 0.5f; // D��man h�z �arpan�

    public void ActivateSlowEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyMovement>().speed *= enemySpeedMultiplier;
        }

        Invoke("DeactivateSlowEnemies", slowDuration);
    }

    private void DeactivateSlowEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyMovement>().speed /= enemySpeedMultiplier;
        }
    }
}

