using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBoundsX : MonoBehaviour
{
    private static readonly float _leftLimit = -40;
    private static readonly float _bottomLimit = -5;


    void Update()
    {
        // Destroy dogs if x position less than left limit
        if (transform.position.x < _leftLimit)
        {
            Destroy(gameObject);
        } 
        // Destroy balls if y position is less than bottomLimit
        else if (transform.position.y < _bottomLimit)
        {
            Destroy(gameObject);
        }
    }
}
