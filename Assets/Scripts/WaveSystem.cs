using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Wave
{
    public List<Transform> enemies;
    public float spawnInterval;
}

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private List<Wave> waves;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxWave;
    [SerializeField] private TextMeshProUGUI waveText;
    private bool _waveCanStart;
    private int _currentWave;
    private Enemies[] _enemies;


    private void Start()
    {
        
        _currentWave = 1;
        _waveCanStart = true;
         waveText.text = "Wave: " + _currentWave;
       
    }

    private void Update()
    {
        _enemies = FindObjectsOfType<Enemies>();
        if (_waveCanStart && _currentWave < maxWave)
        {
            _waveCanStart = false;
            if (_currentWave < waves.Count )
                StartCoroutine(SpawnWave(_currentWave));
            
           
        }
        else if (!_waveCanStart && _enemies.Length <= 0)
        {
            _waveCanStart = true;
        }

        Debug.Log(_enemies.Length);
        Debug.Log(_waveCanStart);
    }

    IEnumerator SpawnWave(int waveIndex)
    {
        Wave wave = waves[waveIndex];
        for (int i = 0; i < wave.enemies.Count; i++)
        {
            Instantiate(wave.enemies[i].gameObject, spawnPoint.position, Quaternion.identity);
            /*spawnPoint.position += new Vector3(2f, 0, 0);*/
            yield return new WaitForSeconds(wave.spawnInterval);
        }
        _currentWave++;
         waveText.text = "Wave: " + _currentWave;
    }
}