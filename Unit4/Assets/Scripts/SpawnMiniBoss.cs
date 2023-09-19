using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMiniBoss : MonoBehaviour
{
    public GameObject MiniBoss;
    public float SpawnPeriod = 2.0f;


    void Start()
    {
        InvokeRepeating(nameof(Spawn), SpawnPeriod, SpawnPeriod);
    }

    
    private void Spawn()
    {
        Vector3 spawnPos = SpawnPosition.Generate();
        Instantiate(MiniBoss, spawnPos, MiniBoss.transform.rotation);
    }
}
