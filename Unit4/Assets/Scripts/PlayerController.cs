using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5.0f;
    private bool _hasPowerup = false;
    private PowerType _powerType;
    private static readonly float _powerupStrength = 15.0f;

    private Rigidbody _rigidbody;

    private GameObject _focalPoint;
    private GameObject _powerupIndicator;

    public GameObject ProjectilePrefab;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
        _powerupIndicator = GameObject.Find("Powerup Indicator");
        _powerupIndicator.SetActive(false);
    }


    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(_focalPoint.transform.forward * (Speed * forwardInput));

        _powerupIndicator.transform.position = transform.position + new Vector3(0, -0.25f, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            StopCoroutine(nameof(PowerupCountdownRoutine));
            StopCoroutine(nameof(PowerupProjectileRoutine));
            
            var powerup = other.gameObject.GetComponent<Powerup>();
            _powerType = powerup.PowerType;

            activatePowerup(true);

            Destroy(other.gameObject);

            switch(_powerType)
            {
                case PowerType.Superpower:
                    StartCoroutine(PowerupCountdownRoutine());
                    break;
                case PowerType.Projectiles:
                    StartCoroutine(PowerupProjectileRoutine());
                    break;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        var otherGameObject = collision.gameObject;

        if (otherGameObject.CompareTag("Enemy") && _hasPowerup && _powerType == PowerType.Superpower)
        {
            var enemyRigidbody = otherGameObject.GetComponent<Rigidbody>();
            var awayFromPlayer = otherGameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * _powerupStrength, ForceMode.Impulse);
        }
    }


    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        activatePowerup(false);
    }


    private IEnumerator PowerupProjectileRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
        }
        activatePowerup(false);
    }



    private void activatePowerup(bool value)
    {
        _hasPowerup = value;
        _powerupIndicator.SetActive(_hasPowerup);
    }
}
