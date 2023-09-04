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

        if (z > _topBound ||
            z < _lowerBound ||
            x > _rightBound ||
            x < _leftBound
            )
        {
            Destroy(gameObject); 
        }
    }
}
