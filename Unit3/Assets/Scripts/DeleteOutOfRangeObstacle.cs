using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOutOfRangeObstacle : MonoBehaviour
{
    private static readonly float _xBound = -10.0f;


    void Update()
    {
        if(transform.position.x < _xBound)
        {
            Destroy(this.gameObject);
        }
        
    }
}
