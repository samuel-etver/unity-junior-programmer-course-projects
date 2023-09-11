using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject EnemyPrefab;

    private static readonly float _spawnRange = 9;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPos = GenerateSpawnPosition();

        Instantiate(EnemyPrefab, spawnPos,  EnemyPrefab.transform.rotation);
    }


    void Update()
    {
        
    }


    private Vector3 GenerateSpawnPosition() 
    {
        float spawnPosX = Random.Range(-_spawnRange, _spawnRange);
        float spawnPosZ = Random.Range(-_spawnRange, _spawnRange);

        return new (spawnPosX, 0, spawnPosZ);
    }
}
