using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 _startPosition;
    private float _repeatWidth;


    void Start()
    {
        _startPosition = transform.position;
        _repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }


    void Update()
    {
        if (transform.position.x < _startPosition.x - _repeatWidth)
        {
            transform.position = _startPosition;
        }
        
    }
}
