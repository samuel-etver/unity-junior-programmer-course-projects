using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Background;
    public GameObject[] ObstaclePrefabs;

    private float _obstacleSpeed = 3;
    
    private static readonly Vector3 _spawnPos = new (25, 1, 0);
    private static readonly float _startDelay = Config.StartupDelay;
    private static readonly float _repeatDelay = 2.0f;


    private void Start()
    {
        var moveLeftComponent = Background.GetComponent<MoveLeft>();
        _obstacleSpeed = moveLeftComponent.Speed;
        InvokeRepeating(nameof(SpawnObstacle), _startDelay, _repeatDelay);
    }


    void Update()
    {
        
    }


    private void SpawnObstacle() 
    {
        int prefabIndex = Random.Range(0, ObstaclePrefabs.Length);
        var obstaclePrefab = ObstaclePrefabs[prefabIndex];
        var obstacleGameObject = Instantiate(obstaclePrefab, _spawnPos, obstaclePrefab.transform.rotation);
        var moveLeftComponent = obstacleGameObject.GetComponent<MoveLeft>();
        moveLeftComponent.Speed = _obstacleSpeed;
    }
}
