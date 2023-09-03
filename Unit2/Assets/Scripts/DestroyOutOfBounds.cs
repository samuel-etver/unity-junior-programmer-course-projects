using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private static readonly float _topBound = 30.0f;
    private static readonly float _lowerBound = -10.0f;
    private static readonly float _rightBound = 30.0f;
    private static readonly float _leftBound = -_rightBound;

    void Update()
    {
        float z = transform.position.z;
        float x = transform.position.x;

        bool playerLiveOver = false;

        if (z > _topBound ||
            (playerLiveOver = (z < _lowerBound)) ||
            x > _rightBound ||
            x < _leftBound
            )
        {
            if(playerLiveOver && TryGetComponent<Animal>(out var animal))
            {
                var lives = animal.GameManager.GetComponent<PlayerLives>();
                lives.DecLive();
                if (lives.Dead) 
                {
                    Debug.Log("Game Over !");
                }
            }

            Destroy(gameObject); 
        }
    }
}
