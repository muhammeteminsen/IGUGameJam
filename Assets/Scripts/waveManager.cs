using UnityEngine;
using UnityEngine.UI;  // UI için gerekli

public class WaveManager : MonoBehaviour
{
    public Text waveText;  // UI Text bileþeni
    public GameObject enemyPrefab;  // Düþman prefab'ý
    public Transform[] spawnPoints;  // Düþmanlarýn spawn olacaðý noktalar
    public float timeBetweenWaves = 5f;  // Dalga geçiþleri arasýndaki süre
    public int enemiesPerWave = 5;  // Her dalgada spawn olacak düþman sayýsý

    private int currentWave = 1;  // Baþlangýç dalgasý
    private bool isWaveInProgress = false;

    void Start()
    {
        // Ýlk dalgayý baþlat
        StartWave();
    }

    void Update()
    {
        if (!isWaveInProgress)
        {
            // Eðer mevcut dalga bitmiþse, bir sonraki dalgayý baþlat
            waveText.text = "Dalga " + currentWave;
            Invoke("StartWave", timeBetweenWaves);  // Dalga baþlama gecikmesi
        }
    }

    // Dalga baþlatma fonksiyonu
    void StartWave()
    {
        isWaveInProgress = true;
        waveText.text = "Dalga " + currentWave + " Baþlýyor!";

        // Düþmanlarý spawn et
        SpawnEnemies();

        // Dalga sonrasýnda UI'yý güncelle
        Invoke("WaveComplete", timeBetweenWaves);  // Dalga bitiþi gecikmesi
    }

    // Düþmanlarý spawn etme fonksiyonu
    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave * currentWave; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
        }
    }

    // Dalga tamamlandýðýnda çaðrýlýr
    void WaveComplete()
    {
        isWaveInProgress = false;

        // Dalga sayýsýný arttýr
        currentWave++;

        // UI'yi dalga tamamlandýktan sonra güncelle
        waveText.text = "Dalga " + currentWave;
    }
}
