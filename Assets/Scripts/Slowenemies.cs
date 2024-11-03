using UnityEngine;

public class SlowEnemies : MonoBehaviour
{
    public float slowDuration = 5.0f; // Gücün süresi
    public float enemySpeedMultiplier = 0.5f; // Düþman hýz çarpaný

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

