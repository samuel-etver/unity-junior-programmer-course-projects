using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody _enemyRb;
    private GameObject _playerGoal;


    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _playerGoal = GameObject.Find("Player Goal");
    }


    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (_playerGoal.transform.position - transform.position).normalized;
        _enemyRb.AddForce(lookDirection * (speed * Time.deltaTime));

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }

    }

}
