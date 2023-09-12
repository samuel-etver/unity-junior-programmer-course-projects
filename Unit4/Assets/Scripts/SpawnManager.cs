using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject PowerupPrefab;

    private static readonly float _spawnRange = 9;

    private int _waveNumber = 0;


    private void Update()
    {
        int enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            SpawnPowerup();
            SpawnEnemyWave(++_waveNumber);
        }
    }


    private Vector3 GenerateSpawnPosition() 
    {
        float spawnPosX = Random.Range(-_spawnRange, _spawnRange);
        float spawnPosZ = Random.Range(-_spawnRange, _spawnRange);

        return new (spawnPosX, 0, spawnPosZ);
    }


    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            var spawnPos = GenerateSpawnPosition();
            Instantiate(EnemyPrefab, spawnPos, EnemyPrefab.transform.rotation);
        }
    }


    private void SpawnPowerup()
    {
        var spawnPos = GenerateSpawnPosition();
        Instantiate(PowerupPrefab, spawnPos, PowerupPrefab.transform.rotation);
    }
}
