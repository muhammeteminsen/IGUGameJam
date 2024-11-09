using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro'yu kullanabilmek için bu namespace'i ekle

public class WaveManager : MonoBehaviour
{
    public TextMeshProUGUI waveText;  // TextMeshPro bileþeni
    public GameObject normalEnemyPrefab;  // Normal enemy prefab
    public GameObject mediumEnemyPrefab;  // Medium enemy prefab
    public GameObject bossEnemyPrefab;    // Boss enemy prefab
    public Transform spawnPoint;
    public int currentWave = 0;    // Þu anki dalga
    public int enemiesRemaining = 10;  // Baþlangýçta 10 düþman
    public float timeBetweenWaves = 5f; // Dalga arasýnda geçen süre
    private bool waveInProgress = false;

    // Belirlediðiniz spawn pozisyonlarý

    public Vector3[] spawnPositions;
    void Start()
    {
        // Düþmaný spawn noktasýndaki pozisyonda yarat
        Instantiate(normalEnemyPrefab, spawnPoint.position, Quaternion.identity);

        // Baþlangýçta Wave 1 ve düþman sayýsýný gösteriyoruz
        UpdateWaveText("Wave 1: " + enemiesRemaining + " Enemies Left!");
        StartNewWave();
    }

    void Update()
    {
        // Eðer dalga bitmiþse ve yeni dalga baþlamak üzereyse
        if (!waveInProgress && currentWave < 10)
        {
            Invoke("StartNewWave", timeBetweenWaves);
            waveInProgress = true;
        }
    }

    void StartNewWave()
    {
        // Dalga bitiyor, yeni dalga baþlatýlýyor
        currentWave++;
        enemiesRemaining = currentWave * 10;  // Her dalgada düþman sayýsý 10 artar

        waveInProgress = false;

        // Yeni dalga metnini göster
        UpdateWaveText("Wave " + (currentWave + 1) + ": " + enemiesRemaining + " Enemies Left!");

        // Düþmanlarý spawn et
        SpawnEnemies();

        // Eðer boss dalgasý ise, bossu spawn et
        if (currentWave == 10)
        {
            SpawnBoss();
        }
    }

    // Düþmanlarý spawn etmek için metod
    void SpawnEnemies()
    {
        // Düþman türüne göre prefab seçimi
        GameObject enemyPrefab = normalEnemyPrefab;

        if (currentWave > 5 && currentWave < 10)
        {
            enemyPrefab = mediumEnemyPrefab; // Medium enemy prefab'ýný seç
        }

        // Düþmanlarý spawn et
        for (int i = 0; i < enemiesRemaining; i++)
        {
            Vector3 spawnPos = spawnPositions[Random.Range(0, spawnPositions.Length)]; // Rastgele bir spawn pozisyonu seç
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity); // Düþmanlarý belirli bir pozisyonda yerleþtir
        }
    }

    // Boss düþmaný spawn etme
    void SpawnBoss()
    {
        Vector3 spawnPos = spawnPositions[Random.Range(0, spawnPositions.Length)]; // Boss için rastgele spawn pozisyonu
        Instantiate(bossEnemyPrefab, spawnPos, Quaternion.identity); // Bossu spawn et
        Debug.Log("Boss Enemy Spawned!");
    }

    // Wave Text'in güncellenmesi için yardýmcý metod
    void UpdateWaveText(string text)
    {
        waveText.text = text;
    }
}
