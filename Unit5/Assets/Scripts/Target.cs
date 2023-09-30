using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameManager GameManager;
    public ParticleSystem ExplosionParticle;

    public int PointValue = 5;

    private Rigidbody _rigidbody;

    private static readonly float _minSpeed = 12.0f;
    private static readonly float _maxSpeed = 16.0f;
    private static readonly float _maxTorque = 10.0f;
    private static readonly float _xRange = 4.0f;
    private static readonly float _ySpawnPos = 0.0f;


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
        DestroyTarget();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Bad") == false)
        {
            GameManager.DecLives();
        }
        Destroy(gameObject);
    }


    public void DestroyTarget()
    {
        if (GameManager.isGameActive)
        {
            Instantiate(ExplosionParticle, transform.position, ExplosionParticle.transform.rotation);
            Destroy(gameObject);
            GameManager.UpdateScore(PointValue);
        }
    }
}
