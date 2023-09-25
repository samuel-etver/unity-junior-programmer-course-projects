using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody _playerRb;
    private GameObject _focalPoint;
    private static readonly float _speed = 500;

    public GameObject powerupIndicator;
    private bool _hasPowerup;
    private static readonly int _powerUpDuration = 5;

    private static readonly float _normalStrength = 10; // how hard to hit enemy without powerup
    private static readonly float _powerupStrength = 25; // how hard to hit enemy with powerup
    

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
    }


    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        _playerRb.AddForce(_focalPoint.transform.forward * (verticalInput * _speed * Time.deltaTime)); 

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
    }


    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            _hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(nameof(PowerupCooldown)); 
        }
    }


    // Coroutine to count down powerup duration
    private IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(_powerUpDuration);
        _hasPowerup = false;
        powerupIndicator.SetActive(false);
    }


    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position; 
           
            if (_hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * _powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * _normalStrength, ForceMode.Impulse);
            }
        }
    }



}
