using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftX : MonoBehaviour
{
    public float Speed;
    private PlayerControllerX _playerControllerScript;
    private static readonly float _leftBound = -10;


    void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }


    void Update()
    {
        // If game is not over, move to the left
        if (_playerControllerScript.GameOver == false)
        {
            transform.Translate(Vector3.left * (Speed * Time.deltaTime), Space.World);
        }

        // If object goes off screen that is NOT the background, destroy it
        if (transform.position.x < _leftBound && !gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }
    }
}
