using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public GameManager GameManager;

    public float Speed = 30.0f;



    void Update()
    {
        if (GameManager.GameOver == false)
        {
            float speedFactor =
              GameManager.SuperSpeed ? 2.0f : 1.0f;
            transform.Translate(Vector3.left * (Time.deltaTime * Speed * speedFactor));
        }
    }
}
