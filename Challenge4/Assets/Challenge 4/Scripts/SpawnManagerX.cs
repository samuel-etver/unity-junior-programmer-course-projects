using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject PowerupPrefab;

    private static readonly float _spawnRangeX = 10;
    private static readonly float _spawnZMin = 15; // set min spawn Z
    private static readonly float _spawnZMax = 25; // set max spawn Z

    private int _waveCount = 1;

    public GameObject Player; 


    void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
        {
            SpawnEnemyWave(_waveCount);
        }

    }


    // Generate random spawn position for powerups and enemy balls
    private Vector3 GenerateSpawnPosition ()
    {
        float xPos = Random.Range(-_spawnRangeX, _spawnRangeX);
        float zPos = Random.Range(_spawnZMin, _spawnZMax);
        return new (xPos, 0, zPos);
    }


    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        Vector3 powerupSpawnOffset = new (0, 0, -15); // make powerups spawn at player end

        // If no powerups remain, spawn a powerup
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
        {
            Instantiate(PowerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, PowerupPrefab.transform.rotation);
        }

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(EnemyPrefab, GenerateSpawnPosition(), EnemyPrefab.transform.rotation);
        }

        _waveCount++;
        ResetPlayerPosition(); // put player back at start

    }


    // Move player back to position in front of own goal
    private void ResetPlayerPosition ()
    {
        Player.transform.position = new Vector3(0, 1, -7);
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
