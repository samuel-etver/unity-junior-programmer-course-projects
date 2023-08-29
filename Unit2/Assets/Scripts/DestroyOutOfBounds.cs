using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float _topBound = 30.0f;
    private float _lowerBound = -10;

    void Update()
    {
        float z = transform.position.z;        

        if (z > _topBound ||
            z < _lowerBound)
        {
            if (z < _lowerBound)
            {
                Debug.Log("Game Over !");
            }
            Destroy(gameObject); 
        }
    }
}
