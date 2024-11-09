using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro'yu kullanabilmek i�in bu namespace'i ekle

public class WaveManager : MonoBehaviour
{
    public TextMeshProUGUI waveText;  // TextMeshPro bile�eni
    public GameObject normalEnemyPrefab;  // Normal enemy prefab
    public GameObject mediumEnemyPrefab;  // Medium enemy prefab
    public GameObject bossEnemyPrefab;    // Boss enemy prefab
    public Transform spawnPoint;
    public int currentWave = 0;    // �u anki dalga
    public int enemiesRemaining = 10;  // Ba�lang��ta 10 d��man
    public float timeBetweenWaves = 5f; // Dalga aras�nda ge�en s�re
    private bool waveInProgress = false;

    // Belirledi�iniz spawn pozisyonlar�

    public Vector3[] spawnPositions;
    void Start()
    {
        // D��man� spawn noktas�ndaki pozisyonda yarat
        Instantiate(normalEnemyPrefab, spawnPoint.position, Quaternion.identity);

        // Ba�lang��ta Wave 1 ve d��man say�s�n� g�steriyoruz
        UpdateWaveText("Wave 1: " + enemiesRemaining + " Enemies Left!");
        StartNewWave();
    }

    void Update()
    {
        // E�er dalga bitmi�se ve yeni dalga ba�lamak �zereyse
        if (!waveInProgress && currentWave < 10)
        {
            Invoke("StartNewWave", timeBetweenWaves);
            waveInProgress = true;
        }
    }

    void StartNewWave()
    {
        // Dalga bitiyor, yeni dalga ba�lat�l�yor
        currentWave++;
        enemiesRemaining = currentWave * 10;  // Her dalgada d��man say�s� 10 artar

        waveInProgress = false;

        // Yeni dalga metnini g�ster
        UpdateWaveText("Wave " + (currentWave + 1) + ": " + enemiesRemaining + " Enemies Left!");

        // D��manlar� spawn et
        SpawnEnemies();

        // E�er boss dalgas� ise, bossu spawn et
        if (currentWave == 10)
        {
            SpawnBoss();
        }
    }

    // D��manlar� spawn etmek i�in metod
    void SpawnEnemies()
    {
        // D��man t�r�ne g�re prefab se�imi
        GameObject enemyPrefab = normalEnemyPrefab;

        if (currentWave > 5 && currentWave < 10)
        {
            enemyPrefab = mediumEnemyPrefab; // Medium enemy prefab'�n� se�
        }

        // D��manlar� spawn et
        for (int i = 0; i < enemiesRemaining; i++)
        {
            Vector3 spawnPos = spawnPositions[Random.Range(0, spawnPositions.Length)]; // Rastgele bir spawn pozisyonu se�
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity); // D��manlar� belirli bir pozisyonda yerle�tir
        }
    }

    // Boss d��man� spawn etme
    void SpawnBoss()
    {
        Vector3 spawnPos = spawnPositions[Random.Range(0, spawnPositions.Length)]; // Boss i�in rastgele spawn pozisyonu
        Instantiate(bossEnemyPrefab, spawnPos, Quaternion.identity); // Bossu spawn et
        Debug.Log("Boss Enemy Spawned!");
    }

    // Wave Text'in g�ncellenmesi i�in yard�mc� metod
    void UpdateWaveText(string text)
    {
        waveText.text = text;
    }
}
