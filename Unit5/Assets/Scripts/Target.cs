using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private static readonly float _minSpeed = 12.0f;
    private static readonly float _maxSpeed = 16.0f;
    private static readonly float _maxTorque = 10.0f;
    private static readonly float _xRange = 4.0f;
    private static readonly float _ySpawnPos = 6.0f;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        _rigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());
        transform.position = RandomSpawnPos();        
    }


    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(_minSpeed, _maxSpeed);
    }


    private float RandomTorque()
    {
        return Random.Range(-_maxTorque, _maxTorque);
    }


    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-_xRange, _xRange), _ySpawnPos);
    }


    private void OnMouseDown()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
