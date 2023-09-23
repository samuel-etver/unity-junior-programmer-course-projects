using System;
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

    private enum SmashState
    {
        None,
        Up,
        Down
    };

    private SmashState _smashState = SmashState.None;
    private Vector3 _smashPosition;
    private static readonly float _smashDuration = 0.3f;
    private static readonly float _smashSpeed = 25.0f;
    private static readonly float _smashForce = 250.0f;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
        _powerupIndicator = GameObject.Find("Powerup Indicator");
        _powerupIndicator.SetActive(false);
    }


    void Update()
    {
        if (_smashState == SmashState.None)
        {
            float forwardInput = Input.GetAxis("Vertical");
            _rigidbody.AddForce(_focalPoint.transform.forward * (Speed * forwardInput));
        }
        else
        {
            float direction = _smashState == SmashState.Up ? 1 : -1;
            _smashPosition += new Vector3(0, Time.deltaTime * direction * _smashSpeed, 0);
            transform.position = _smashPosition;
        }

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
            ForceEnemy(otherGameObject, _powerupStrength);
        }
    }


    private void ForceEnemy(GameObject enemy, float strength, bool distanceDependent = false)
    {
        var enemyRigidbody = enemy.GetComponent<Rigidbody>();
        var awayFromPlayer = enemy.transform.position - transform.position;
        float distanceK = 1.0f;
        if (distanceDependent)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            distanceK = (float)(1.0f / Math.Pow(1.0f + distance, 2.0f));
            Debug.Log(distanceK);
        }
        var force = awayFromPlayer * (strength * distanceK);
        enemyRigidbody.AddForce(force, ForceMode.Impulse);
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


    private GameObject[] FindEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy");
    }


    private void ShootToEnemies()
    {
        GameObject[] enemies = FindEnemies();

        for (int i = 0; i < enemies.Length; i++)
        {
            ShootToEnemy(enemies[i]);
        }
    }


    private IEnumerator PowerupSmushRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            _smashPosition = this.transform.position;
            _smashState = SmashState.Up;
            yield return new WaitForSeconds(_smashDuration / 2);

            void Smash()
            {
                GameObject[] enemies = FindEnemies();
                for (int i = 0; i < enemies.Length; i++)
                {
                    ForceEnemy(enemies[i], _smashForce, true);
                }
            };

            Smash();

            _smashState = SmashState.Down;
            yield return new WaitForSeconds(_smashDuration / 2);

            _smashState = SmashState.None;
            yield return new WaitForSeconds(2.0f);
        }
        ActivatePowerup(false);
    }



    private void ActivatePowerup(bool value)
    {
        _hasPowerup = value;
        _powerupIndicator.SetActive(_hasPowerup);
    }
}
