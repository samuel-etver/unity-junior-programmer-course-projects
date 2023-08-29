using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] AnimalPrefabs;

    private float _spawnRangeX = 20;
    private float _spawnPosZ = 20;

    private float _startDelay = 2;
    private float _spawnInterval = 1.5f;


    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", _startDelay, _spawnInterval);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnRandomAnimal();
        }        
    }


    private void SpawnRandomAnimal()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-_spawnRangeX, _spawnRangeX), 0, _spawnPosZ);

        int animalIndex = Random.Range(0, AnimalPrefabs.Length);
        Instantiate(AnimalPrefabs[animalIndex],
                    spawnPos,
                    AnimalPrefabs[animalIndex].transform.rotation);
    }
}
