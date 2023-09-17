using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;

    private Rigidbody _rigidbody;
    private GameObject _player;


    void Start()
    {
        _player = GameObject.Find("Player");
        _rigidbody = gameObject.GetComponent<Rigidbody>();            
    }


    void Update()
    {
        Vector3 lookDirection = (_player.transform.position - transform.position).normalized;

        _rigidbody.AddForce(lookDirection * Speed);       

        if (transform.position.y < -10.0f)
        {
            Destroy(gameObject);
        }
    }
}
