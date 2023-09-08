using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftDelay : MonoBehaviour
{
    private MoveLeft _moveLeft;
    private float _speed;


    private void Awake()
    {
        _moveLeft = GetComponent<MoveLeft>();
    }


    void Start()
    {
        _speed = _moveLeft.Speed;
        _moveLeft.Speed = 0.0f;
        Invoke(nameof(StartupMove), Config.StartupDelay);
    }


    private void StartupMove()
    {
        _moveLeft.Speed = _speed;
    }
}
