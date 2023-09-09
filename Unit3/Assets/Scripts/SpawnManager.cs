using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Background;
    public GameObject[] ObstaclePrefabs;
    
    private static readonly Vector3 _spawnPos = new (25, 1, 0);
    private static readonly float _startDelay = Config.StartupDelay;
    private static readonly float _repeatDelay = 2.0f;


    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), _startDelay + 0.5f, _repeatDelay);
    }


    private void SpawnObstacle() 
    {
        int prefabIndex = Random.Range(0, ObstaclePrefabs.Length);
        var obstaclePrefab = ObstaclePrefabs[prefabIndex];
        var obstacleGameObject = Instantiate(obstaclePrefab, _spawnPos, obstaclePrefab.transform.rotation);
        var moveLeftComponent = obstacleGameObject.GetComponent<MoveLeft>();
        moveLeftComponent.Speed = Background.GetComponent<MoveLeft>().Speed;
    }
}
