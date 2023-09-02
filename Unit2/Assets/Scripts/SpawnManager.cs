using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] AnimalPrefabs;

    private static readonly float _spawnRangeMaxX = 20.0f;
    private static readonly float _spawnRangeMinX = -_spawnRangeMaxX;
    private static readonly float _spawnFromTopPosZ = 20.0f;

    private static readonly float _spawnRangeMaxZ = 20.0f;
    private static readonly float _spawnRangeMinZ = 10.0f;
    private static readonly float _spawnFromRightPosX = 10;
    private static readonly float _spawnFromLeftPosX = -_spawnFromRightPosX;

    private readonly float _startDelay = 2;
    private readonly float _spawnInterval = 1.5f;


    void Start()
    {
        //InvokeRepeating(nameof(SpawnRandomAnimalFromTop), _startDelay, _spawnInterval);
        StartCoroutine(SpawnRandomAgressiveAnimal());
    }


    private GameObject SpawnRandomAnimal(Vector3 spawnPos, bool agressive)
    {
        int animalIndex = Random.Range(0, AnimalPrefabs.Length);
        var animalGameObject = Instantiate(AnimalPrefabs[animalIndex],
                                           spawnPos,
                                           AnimalPrefabs[animalIndex].transform.rotation);
        var animal = animalGameObject.GetComponent<Animal>();
        animal.Agressive = agressive;

        return animalGameObject;
    }


    private void SpawnRandomAnimalFromTop()
    {
        Vector3 spawnPos = new(Random.Range(_spawnRangeMinX, _spawnRangeMaxX), 0, _spawnFromTopPosZ);
        SpawnRandomAnimal(spawnPos, false);
    }


    private void SpawnRandomAnimalFromLeft()
    {
        Vector3 spawnPos = new(_spawnFromLeftPosX, 0, Random.Range(_spawnRangeMinZ, _spawnRangeMaxZ));
        var animalGameObject = SpawnRandomAnimal(spawnPos, false);
        animalGameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
    }


    private void SpawnRandomAnimalFromRight()
    {
        Vector3 spawnPos = new(_spawnFromRightPosX, 0, Random.Range(_spawnRangeMinZ, _spawnRangeMaxZ));
        var animalGameObject = SpawnRandomAnimal(spawnPos, false);
        animalGameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
    }


    private IEnumerator SpawnRandomAgressiveAnimal()
    {
        yield return new WaitForSeconds(_spawnInterval / 2.0f);
        while(true)
        {
            SpawnRandomAnimalFromLeft();
            yield return new WaitForSeconds(_spawnInterval);
            SpawnRandomAnimalFromRight();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}
