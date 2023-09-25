using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    private static readonly float _speed = 200;
    public GameObject Player;


    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * _speed * Time.deltaTime);

        transform.position = Player.transform.position; // Move focal point with player
    }
}
