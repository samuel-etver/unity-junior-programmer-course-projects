using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] BallPrefabs;

    private static readonly float _spawnLimitXLeft = -22;
    private static readonly float _spawnLimitXRight = 7;
    private static readonly float _spawnPosY = 30;

    private static readonly float _startDelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnRandomBall), _startDelay, Random.Range(3.0f, 5.0f));
    }


    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall ()
    {
        // Generate random ball index and random spawn position
        Vector3 spawnPos = new (Random.Range(_spawnLimitXLeft, _spawnLimitXRight), _spawnPosY, 0);

        // instantiate ball at random spawn location
        int ballIndex = Random.Range(0, BallPrefabs.Length);
        Instantiate(BallPrefabs[ballIndex], spawnPos, BallPrefabs[0].transform.rotation);
    }

}
