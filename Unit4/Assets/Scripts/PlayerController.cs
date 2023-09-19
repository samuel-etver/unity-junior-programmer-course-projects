using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    ShootToEnemies();
        //}
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            StopCoroutine(nameof(PowerupCountdownRoutine));
            StopCoroutine(nameof(PowerupProjectileRoutine));
            
            var powerup = other.gameObject.GetComponent<Powerup>();
            _powerType = powerup.PowerType;

            ActivatePowerup(true);

            Destroy(other.gameObject);

            switch(_powerType)
            {
                case PowerType.Superpower:
                    StartCoroutine(PowerupCountdownRoutine());
                    break;
                case PowerType.Projectiles:
                    StartCoroutine(PowerupProjectileRoutine());
                    break;
                case PowerType.Smash:
                    StartCoroutine(PowerupSmushRoutine());
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
        ActivatePowerup(false);
    }


    private IEnumerator PowerupProjectileRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
            ShootToEnemies();
        }
        ActivatePowerup(false);
    }


    private void ShootToEnemy(GameObject enemy)
    {
        var projectile = Instantiate(ProjectilePrefab);
        projectile.transform.position = transform.position;
        projectile.transform.LookAt(new Vector3(enemy.transform.position.x,
                                                projectile.transform.position.y,
                                                enemy.transform.position.z));
        projectile.transform.Rotate(90, 0, 0);
    }


    private void ShootToEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            ShootToEnemy(enemies[i]);
        }
    }


    private IEnumerator PowerupSmushRoutine()
    {
        yield return null;
    }


    private void ActivatePowerup(bool value)
    {
        _hasPowerup = value;
        _powerupIndicator.SetActive(_hasPowerup);
    }
}
