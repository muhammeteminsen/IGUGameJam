using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public GameObject normalEnemyPrefab;
    public GameObject mediumEnemyPrefab;
    public GameObject bossPrefab;
    public Transform[] spawnPoints;
    public int startEnemyCount = 10;
    public float spawnDelay = 1f;
    public int totalWaves = 10;

    private int currentWave = 1;

    void Start()
    {
        if (normalEnemyPrefab != null && mediumEnemyPrefab != null && bossPrefab != null && spawnPoints.Length > 0)
        {
            StartCoroutine(SpawnWave());
        }
        else
        {
            Debug.LogError("Prefab veya spawn noktalarý eksik! Lütfen tüm deðerlerin ayarlandýðýndan emin olun.");
        }
    }

    IEnumerator SpawnWave()
    {
        while (currentWave <= totalWaves)
        {
            int enemyCount = startEnemyCount + (currentWave - 1) * 10;

            for (int i = 0; i < enemyCount; i++)
            {
                if (currentWave < 4)
                {
                    SpawnEnemy(normalEnemyPrefab);
                }
                else if (currentWave < totalWaves)
                {
                    SpawnEnemy(mediumEnemyPrefab);
                }
                yield return new WaitForSeconds(spawnDelay);
            }

            yield return new WaitForSeconds(5f);
            currentWave++;
        }

        SpawnBoss();
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        if (enemyPrefab != null)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    void SpawnBoss()
    {
        if (bossPrefab != null)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}

