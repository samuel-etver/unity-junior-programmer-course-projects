using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float Speed = 30.0f;


    void Update()
    {
        transform.Translate(Vector3.left * (Time.deltaTime * Speed));
    }
}
