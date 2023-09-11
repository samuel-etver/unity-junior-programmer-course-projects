using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5.0f;
    public bool HasPowerup = false;
    public float PowerupStrength = 15.0f;

    private Rigidbody _rigidbody;

    private GameObject _focalPoint;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
    }


    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(_focalPoint.transform.forward * (Speed * forwardInput));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            HasPowerup = true;
            Destroy(other.gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        var otherGameObject = collision.gameObject;

        if (otherGameObject.CompareTag("Enemy") && HasPowerup)
        {
            var enemyRigidbody = otherGameObject.GetComponent<Rigidbody>();
        }

    }
}
