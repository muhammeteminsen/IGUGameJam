using UnityEngine;
using UnityEngine.UI;  // UI i�in gerekli

public class WaveManager : MonoBehaviour
{
    public Text waveText;  // UI Text bile�eni
    public GameObject enemyPrefab;  // D��man prefab'�
    public Transform[] spawnPoints;  // D��manlar�n spawn olaca�� noktalar
    public float timeBetweenWaves = 5f;  // Dalga ge�i�leri aras�ndaki s�re
    public int enemiesPerWave = 5;  // Her dalgada spawn olacak d��man say�s�

    private int currentWave = 1;  // Ba�lang�� dalgas�
    private bool isWaveInProgress = false;

    void Start()
    {
        // �lk dalgay� ba�lat
        StartWave();
    }

    void Update()
    {
        if (!isWaveInProgress)
        {
            // E�er mevcut dalga bitmi�se, bir sonraki dalgay� ba�lat
            waveText.text = "Dalga " + currentWave;
            Invoke("StartWave", timeBetweenWaves);  // Dalga ba�lama gecikmesi
        }
    }

    // Dalga ba�latma fonksiyonu
    void StartWave()
    {
        isWaveInProgress = true;
        waveText.text = "Dalga " + currentWave + " Ba�l�yor!";

        // D��manlar� spawn et
        SpawnEnemies();

        // Dalga sonras�nda UI'y� g�ncelle
        Invoke("WaveComplete", timeBetweenWaves);  // Dalga biti�i gecikmesi
    }

    // D��manlar� spawn etme fonksiyonu
    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave * currentWave; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
        }
    }

    // Dalga tamamland���nda �a�r�l�r
    void WaveComplete()
    {
        isWaveInProgress = false;

        // Dalga say�s�n� artt�r
        currentWave++;

        // UI'yi dalga tamamland�ktan sonra g�ncelle
        waveText.text = "Dalga " + currentWave;
    }
}
