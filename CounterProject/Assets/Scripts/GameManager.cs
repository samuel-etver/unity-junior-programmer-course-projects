using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text InBoxText;
    public TMP_InputField SpawnInput;
    public GameObject Ball;

    [HideInInspector]
    [NonSerialized]
    public int SpawnCount;

    [HideInInspector]
    [NonSerialized]
    public int InBox;


    private static readonly float _spawnBoundYMax = 14.0f;
    private static readonly float _spawnBoundYMin = 8.0f;
    private static readonly float _spawnBoundXMax = 3.0f;
    private static readonly float _spawnBoundXMin = -_spawnBoundXMax;
    private static readonly float _spawnBoundZMax = _spawnBoundXMax;
    private static readonly float _spawnBoundZMin = -_spawnBoundXMax;


    void Start()
    {
        SpawnCount =
        InBox = 0;

        SpawnInput.text = SpawnCount.ToString();
    }


    void Update()
    {
        UpdateInBox();
    }


    public void OnSpawnButtonClick()
    {
        DestroyBalls();

        InBox = 0;

        int count = 0;
        try
        {
            count = Int32.Parse(SpawnInput.text);
        } 
        catch
        {
        }
        SpawnCount = count < 0 ? 0 : count;
        SpawnBalls(SpawnCount);
    }


    private void SpawnBalls(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(Ball, GetSpawnPos(), Ball.transform.rotation);
        }
    }


    private Vector3 GetSpawnPos()
    {
        return new(
            UnityEngine.Random.Range(_spawnBoundXMin, _spawnBoundXMax),
            UnityEngine.Random.Range(_spawnBoundYMin, _spawnBoundYMax),
            UnityEngine.Random.Range(_spawnBoundZMin, _spawnBoundZMax)
        );
    }


    private void UpdateInBox()
    {
        InBoxText.text = "In Box : " + InBox;
    }


    private void DestroyBalls()
    {
        var allBalls = GameObject.FindObjectsOfType<Ball>();
        for(int i = 0; i < allBalls.Length; i++)
        {
            Destroy(allBalls[i].gameObject);
        }
    }
}
