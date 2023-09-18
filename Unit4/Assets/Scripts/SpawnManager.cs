using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    public GameObject BossPrefab;
    public GameObject PowerupPrefab;

    private static readonly float _spawnRange = 9;
    private static readonly int _spawnBossEveryNWave = 0;

    private int _waveNumber = 0;


    private void Update()
    {
        int enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            SpawnPowerup();

            var regularEnemiesCount = ++_waveNumber;
            if ((_waveNumber - 1) % (1 + _spawnBossEveryNWave) == _spawnBossEveryNWave)
            {
                SpawnBoss();
                regularEnemiesCount--;
            }
            SpawnEnemyWave(regularEnemiesCount);
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
            var prefabIndex = Random.Range(0, EnemyPrefabs.Length);
            var prefab = EnemyPrefabs[prefabIndex];
            var spawnPos = GenerateSpawnPosition();
            Instantiate(prefab, spawnPos, prefab.transform.rotation);
        }
    }


    private void SpawnBoss()
    {
        var spawnPos = GenerateSpawnPosition();
        Instantiate(BossPrefab, spawnPos, BossPrefab.transform.rotation);
    }


    private void SpawnPowerup()
    {
        var powerTypeValues = System.Enum.GetValues(typeof(PowerType));
        var powerTypeIndex = Random.Range(0, powerTypeValues.Length);
        var powerType = powerTypeValues.GetValue(powerTypeIndex).ConvertTo<PowerType>();

        var spawnPos = GenerateSpawnPosition();

        var gameObject = Instantiate(PowerupPrefab, spawnPos, PowerupPrefab.transform.rotation);
        var powerupComponent = gameObject.GetComponent<Powerup>();
        powerupComponent.PowerType = PowerType.Projectiles;// powerType;
    }
}
